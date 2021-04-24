using UnityEngine;

public class FaceTowardsCamera : MonoBehaviour
{
    private Camera _cam;
    
    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        transform.rotation = _cam.transform.rotation;
    }
}