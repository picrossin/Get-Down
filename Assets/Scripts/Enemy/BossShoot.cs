using System.Collections;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float waitSeconds = 1f;

    private Conductor _conductor;
    private bool _initialized;
    private GameObject _player;
    private bool _offBeat;
    private bool _waiting = true;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        StartCoroutine(WaitTime());
    }

    private void Update()
    {
        if (!_initialized)
        {
            _initialized = true;
            _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
            _conductor.beat.AddListener(ShootPizza);
            
            _player = GameObject.FindWithTag("Player");
        }
    }

    private void ShootPizza()
    {
        _offBeat = !_offBeat;
        
        if (_player != null && _offBeat && !_waiting)
        {
            Vector3 playerPos2D = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            Vector3 enemyPos2D = new Vector3(transform.position.x, 0, transform.position.z);

            Vector3 bulletDirection = (playerPos2D - enemyPos2D).normalized;

            GameObject bulletInstance = Instantiate(bullet, transform.position + bulletDirection * .05F,
                Quaternion.identity);

            bulletInstance.GetComponent<Rigidbody>()
                .AddForce(bulletDirection * 100f * bulletSpeed);
        }
        else
        {
            _player = GameObject.FindWithTag("Player");
        }
    }
    
    private IEnumerator WaitTime()
    {
        _waiting = true;
        yield return new WaitForSeconds(waitSeconds);
        _waiting = false;
    }
}