using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int bouncesToDestroy = 3;

    private int _bounces;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3)
        {
            _bounces++;
            if (_bounces >= bouncesToDestroy)
            {
                DestroyBullet();
            }
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
