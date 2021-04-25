using System;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int totalHealth = 3;

    private int _currentHealth;
    private Rigidbody _rigidbody;
    
    private void Start()
    {
        _currentHealth = totalHealth;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Record"))
        {
            TakeDamage(other.transform.position);
        }
    }

    private void TakeDamage(Vector3 recordPosition)
    {
        _currentHealth--;
        if (_currentHealth > 0)
        {
            Vector3 recordPosition2D = new Vector3(recordPosition.x, 0, recordPosition.z);
            Vector3 enemyPosition2D = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 knockbackDir = (recordPosition2D - enemyPosition2D).normalized;
            
            _rigidbody.AddForce(-knockbackDir * 50f, ForceMode.Impulse);
        }
        else
        {
            GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>().EnemyKilled();
            Destroy(gameObject);
        }
    }
}
