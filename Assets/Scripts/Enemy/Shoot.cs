using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private int framesBetweenShots;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;

    private int _currentShootFrame;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            _currentShootFrame++;
            if (_currentShootFrame >= framesBetweenShots)
            {
                Vector3 playerPos2D = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
                Vector3 enemyPos2D = new Vector3(transform.position.x, 0, transform.position.z);

                Vector3 bulletDirection = (playerPos2D - enemyPos2D).normalized;

                GameObject bulletInstance = Instantiate(bullet, transform.position + bulletDirection * .05F,
                    Quaternion.identity);

                bulletInstance.GetComponent<Rigidbody>()
                    .AddForce(bulletDirection * 100f * bulletSpeed);
                _currentShootFrame = 0;
            }
        }
        else
        {
            _player = GameObject.FindWithTag("Player");
        }
    }
}