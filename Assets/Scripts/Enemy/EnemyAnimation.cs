using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private string stateNameAnim1;
    [SerializeField] private string stateNameAnim2;
    [SerializeField] private RuntimeAnimatorController controller1;
    [SerializeField] private RuntimeAnimatorController controller2;
    
    private Conductor _conductor;
    private Animator _animator;

    private bool _initialized;
    private string stateName;
    
    private void Start()
    {
        int enemyType = Random.Range(0, 2);
        
        _animator = GetComponent<Animator>();

        if (enemyType == 0)
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

    private void DanceOnBeat()
    {
        _animator.Play(stateName, 0, 0.0f);
    }
}
