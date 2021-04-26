using System.Collections;
using UnityEngine;

public class SpotlightManager : MonoBehaviour
{
    public enum LightAnimations
    {
        DualCorners = 0,
        FourCorners = 1,
        TopBottom = 2,
        LeftRight = 3,
        Boss = 4
    }

    [SerializeField] private LightAnimations currentAnim = LightAnimations.DualCorners;
    [SerializeField] private Light topRight;
    [SerializeField] private Light topLeft;
    [SerializeField] private Light bottomRight;
    [SerializeField] private Light bottomLeft;
    [SerializeField] private float flashIntensity = 35f;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private float buildSpeed = 3;
    [SerializeField] private bool isBoss;

    private int _measureBeat;
    private Conductor _conductor;
    private bool _initialized;
    private LightAnimations[] _animCycle;
    private int _animCycleIndex;

    private void Start()
    {
        topRight.intensity = 0;
        topLeft.intensity = 0;
        bottomRight.intensity = 0;
        bottomLeft.intensity = 0;

        topRight.color = color1;
        topLeft.color = color2;
        bottomRight.color = color2;
        bottomLeft.color = color1;

        _animCycle = new[]
        {
            LightAnimations.DualCorners,
            LightAnimations.FourCorners,
            LightAnimations.TopBottom,
            LightAnimations.LeftRight,
        };

        _animCycleIndex = Random.Range(0, _animCycle.Length);
    }

    private void LateUpdate()
    {
        if (!_initialized)
        {
            _initialized = true;
            _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
            _conductor.beat.AddListener(Flash);
            _measureBeat = (int) Mathf.Floor(_conductor.SongPositionInBeats);
        }
    }

    private void Flash()
    {
        _measureBeat = (_measureBeat + 1) % 4;

        switch (currentAnim)
        {
            case LightAnimations.DualCorners:
                if (_measureBeat == 0 || _measureBeat == 2)
                {
                    StartCoroutine(FlashLight(new[] {bottomLeft, topRight}, 0.2f));
                }
                else if (_measureBeat == 1)
                {
                    StartCoroutine(FlashLight(new[] {bottomRight, topLeft}, 0.2f));
                }
                else if (_measureBeat == 3)
                {
                    StartCoroutine(FlashLight(new[] {bottomLeft, bottomRight, topLeft, topRight}, 0.2f));
                }

                break;
            case LightAnimations.FourCorners:
                if (_measureBeat == 0)
                {
                    StartCoroutine(FlashLight(new[] {topLeft}, 0.2f));
                }
                else if (_measureBeat == 1)
                {
                    StartCoroutine(FlashLight(new[] {topRight}, 0.2f));
                }
                else if (_measureBeat == 2)
                {
                    StartCoroutine(FlashLight(new[] {bottomRight}, 0.2f));
                }
                else if (_measureBeat == 3)
                {
                    StartCoroutine(FlashLight(new[] {bottomLeft, bottomRight, topLeft, topRight}, 0.2f));
                }

                break;
            case LightAnimations.TopBottom:
                if (_measureBeat == 0 || _measureBeat == 2)
                {
                    StartCoroutine(FlashLight(new[] {topLeft, topRight}, 0.2f));
                }
                else if (_measureBeat == 1)
                {
                    StartCoroutine(FlashLight(new[] {bottomRight, bottomLeft}, 0.2f));
                }
                else if (_measureBeat == 3)
                {
                    StartCoroutine(FlashLight(new[] {bottomLeft, bottomRight, topLeft, topRight}, 0.2f));
                }

                break;
            case LightAnimations.LeftRight:
                if (_measureBeat == 0 || _measureBeat == 2)
                {
                    StartCoroutine(FlashLight(new[] {topLeft, bottomLeft}, 0.2f));
                }
                else if (_measureBeat == 1)
                {
                    StartCoroutine(FlashLight(new[] {bottomRight, topRight}, 0.2f));
                }
                else if (_measureBeat == 3)
                {
                    StartCoroutine(FlashLight(new[] {bottomLeft, bottomRight, topLeft, topRight}, 0.2f));
                }

                break;
            case LightAnimations.Boss:
                if (_measureBeat == 1)
                {
                    StartCoroutine(FlashLight(new[] {topLeft, topRight}, 0.4f));
                }
                if (_measureBeat == 2)
                {
                    StartCoroutine(FlashLight(new[] {bottomLeft, bottomRight}, 0.4f));
                }

                break;
        }

        if (!isBoss)
        {
            if (_measureBeat == 3)
            {
                _animCycleIndex++;
                if (_animCycleIndex == _animCycle.Length)
                {
                    _animCycleIndex = 0;
                }

                currentAnim = _animCycle[_animCycleIndex];
            }
        }
    }

    private IEnumerator FlashLight(Light[] lightsToFlash, float onTime)
    {
        float currentIntensity = 0f;
        while (currentIntensity < flashIntensity)
        {
            foreach (Light light in lightsToFlash)
            {
                light.intensity = currentIntensity;
            }

            currentIntensity += buildSpeed;
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitForSeconds(onTime);

        while (currentIntensity > 0)
        {
            foreach (Light light in lightsToFlash)
            {
                light.intensity = currentIntensity;
            }

            currentIntensity -= buildSpeed;
            yield return new WaitForSeconds(0.001f);
        }

        foreach (Light light in lightsToFlash)
        {
            light.intensity = 0;
        }
    }
}