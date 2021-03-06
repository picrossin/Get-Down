using System.Collections;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float sightRange = 4;
    [SerializeField] private float waitSeconds = 1f;
    
    private GameObject _player;
    private bool _chasing;
    private Vector3 _moveDir;
    private bool _waiting = true;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitTime());
    }

    private void Update()
    {
        if (_player != null)
        {
            Vector3 player2DPosition = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            Vector3 enemy2DPosition = new Vector3(transform.position.x, 0, transform.position.z);

            _chasing = Vector3.Distance(player2DPosition, enemy2DPosition) <= sightRange;
            _moveDir = (player2DPosition - enemy2DPosition).normalized;
        }
        else
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void FixedUpdate()
    {
        if (_chasing && !_waiting)
        {
            transform.position += _moveDir / 100 * speed;
        }
    }

    private IEnumerator WaitTime()
    {
        _waiting = true;
        yield return new WaitForSeconds(waitSeconds);
        _waiting = false;
    }
}
