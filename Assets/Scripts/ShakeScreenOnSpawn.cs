using UnityEngine;

public class ShakeScreenOnSpawn : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float intensity;

    private void Start()
    {
        Camera.main.GetComponent<ScreenShake>().Shake(duration, intensity);
    }
}
