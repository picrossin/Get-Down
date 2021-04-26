using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int totalHealth = 3;
    [SerializeField] private float knockbackForce = 50f;
    [SerializeField] private float invincibilityTimeInSeconds = 0.25f;
    [SerializeField] private GameObject onDestroyObject;
    [SerializeField] private GameObject enemyHurt;
    [SerializeField] private GameObject enemyDie;
    [SerializeField] private GameObject hurtParticles;
    
    private int _currentHealth;
    private Rigidbody _rigidbody;
    private bool _invincible;
    private GameObject _inflictingRecord;
    
    private void Start()
    {
        _currentHealth = totalHealth;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Record"))
        {
            TakeDamage(other.transform.position, other.gameObject);
        }
    }

    private void TakeDamage(Vector3 recordPosition, GameObject record)
    {
        Instantiate(hurtParticles, transform.position, Quaternion.identity);
        
        if (!_invincible || record.GetInstanceID() != _inflictingRecord.GetInstanceID())
        {
            _currentHealth--;
            if (_currentHealth > 0)
            {
                Vector3 recordPosition2D = new Vector3(recordPosition.x, 0, recordPosition.z);
                Vector3 enemyPosition2D = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 knockbackDir = (recordPosition2D - enemyPosition2D).normalized;
            
                _rigidbody.AddForce(-knockbackDir * knockbackForce, ForceMode.Impulse);

                StartCoroutine(InvincibilityFrame());
                Instantiate(enemyHurt);

                _inflictingRecord = record;
            }
            else
            {
                Instantiate(enemyDie);
                
                GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>().EnemyKilled();
                if (onDestroyObject != null)
                {
                    Instantiate(onDestroyObject, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator InvincibilityFrame()
    {
        _invincible = true;
        yield return new WaitForSeconds(invincibilityTimeInSeconds);
        _invincible = false;
    }
}
