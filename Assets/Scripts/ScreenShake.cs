using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 _startPosition;
    
    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    public void Shake(float duration, float intensity)
    {
        StartCoroutine(ShakeTiming(duration, intensity));
    }
    
    private IEnumerator ShakeTiming(float duration, float intensity)
    {
        float timeAtIteration = duration / 10;
        for (int i = 0; i < 10; i++)
        {
            transform.localPosition = _startPosition + new Vector3(
                Random.Range(-0.1f, 0.1f) * intensity,
                Random.Range(-0.1f, 0.1f) * intensity,
                Random.Range(-0.1f, 0.1f) * intensity);
            yield return new WaitForSeconds(timeAtIteration);
        }

        transform.localPosition = _startPosition;
    }
}
