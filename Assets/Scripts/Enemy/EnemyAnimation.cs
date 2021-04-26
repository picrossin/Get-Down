using System.Collections;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private string stateNameAnim1;
    [SerializeField] private string stateNameAnim2;
    [SerializeField] private RuntimeAnimatorController controller1;
    [SerializeField] private RuntimeAnimatorController controller2;
    [SerializeField] private Sprite shootFace1;
    [SerializeField] private Sprite shootFace2;
    
    private Conductor _conductor;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool _initialized;
    private string stateName;
    private int _enemyType;
    
    private void Start()
    {
        _enemyType = Random.Range(0, 2);
        
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (_enemyType == 0)
        {
            stateName = stateNameAnim1;
            _animator.runtimeAnimatorController = controller1;
        }
        else
        {
            stateName = stateNameAnim2;
            _animator.runtimeAnimatorController = controller2;
        }
    }

    private void Update()
    {
        if (!_initialized)
        {
            _initialized = true;
            _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
            _conductor.beat.AddListener(DanceOnBeat);
        }
    }

    public void Shoot()
    {
        _animator.speed = 0f;
        _animator.enabled = false;
        _spriteRenderer.sprite = _enemyType == 0 ? shootFace1 : shootFace2;
        StartCoroutine(ShootAnimTiming());
    }

    private IEnumerator ShootAnimTiming()
    {
        yield return new WaitForSeconds(0.2f);
        _animator.speed = 1f;
        _animator.enabled = true;       
    }

    private void DanceOnBeat()
    {
        if (_animator.enabled)
            _animator.Play(stateName, 0, 0.0f);
    }
}
