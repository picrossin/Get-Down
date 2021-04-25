using UnityEngine;

public class Record : MonoBehaviour
{
    [SerializeField] private int totalBounce = 4;
    
    private int _bounces = 0;
    public int Bounces => _bounces;

    private bool _onFloor;
    public bool OnFloor => _onFloor;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3)
        {
            _bounces++;
            if (_bounces >= totalBounce)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<SphereCollider>().isTrigger = true;
                _onFloor = true;
            }
        }
    }
}
