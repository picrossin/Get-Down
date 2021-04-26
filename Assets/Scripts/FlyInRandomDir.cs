using UnityEngine;

public class FlyInRandomDir : MonoBehaviour
{
    private Vector3 _movement = new Vector3(1, 0, 0);
    private float _movementMultiplier = 1f;
    
    private void Start()
    {
        _movement = Quaternion.Euler(0, Random.Range(0, 360), 0) * _movement;
        _movement *= 0.05f;
    }
    
    private void Update()
    {
        transform.position += _movement;
        _movement *= _movementMultiplier;
        _movementMultiplier -= 0.001f;
    }
}
