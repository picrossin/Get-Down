using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Elevator elevator;

    [SerializeField] private AudioClip[] songs;
    [SerializeField] private int[] bpms;

    private int _enemyCount = Int32.MaxValue;
    private Conductor _conductor;
    private bool _trackStarted;
    private int _currentTrack = 0;

    private void Start()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        GameObject conductorObject = GameObject.FindWithTag("Conductor");
        if (conductorObject == null)
        {
            conductorObject = new GameObject();
            conductorObject.name = "Conductor";
            conductorObject.tag = "Conductor";
            conductorObject.AddComponent<Conductor>();
        }

        _conductor = conductorObject.GetComponent<Conductor>();
    }

    private void Update()
    {
        if (!_trackStarted && !_conductor.CurrentTrackStarted)
        {
            _trackStarted = true;
            _conductor.StartTrack(songs[_currentTrack], bpms[_currentTrack]);
        }
    }

    public void EnemyKilled()
    {
        _enemyCount--;

        if (_enemyCount <= 0)
        {
            elevator.Open();
        }
    }

    public void ResetScene()
    {
        GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().PlayDeathTransition();
    }
}
