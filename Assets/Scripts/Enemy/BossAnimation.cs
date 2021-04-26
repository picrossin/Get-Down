using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    [SerializeField] private string animName = "Chomp";
    
    private Conductor _conductor;
    private Animator _animator;

    private bool _initialized;
    private bool offBeat;  
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
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
        offBeat = !offBeat;
        if (_animator.enabled && offBeat)
            _animator.Play(animName, 0, 0.0f);
    }
}
