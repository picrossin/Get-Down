using UnityEngine;

public class Record : MonoBehaviour
{
    private int _bounces = 0;
    public int Bounces => _bounces;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3)
        {
            _bounces++;
        }
    }
}
