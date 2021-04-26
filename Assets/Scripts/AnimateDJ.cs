using UnityEngine;

public class AnimateDJ : MonoBehaviour
{
    private Conductor _conductor;
    private bool _initialized;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_initialized)
        {
            _initialized = true;
            _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
            _conductor.beat.AddListener(PlayAnimOnBeat);
        }
    }

    private void PlayAnimOnBeat()
    {
        _animator.Play("DJ", 0, 0.0f);
    }
}
