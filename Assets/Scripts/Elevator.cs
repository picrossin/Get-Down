using System;
using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool stopTrackThisLevel;
    [SerializeField] private GameObject elevatorBeep;
    [SerializeField] private GameObject elevatorDoor;
    
    private bool _open;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().PlayElevator)
            StartCoroutine(PlayElevatorSounds(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _open)
        {
            Destroy(other.gameObject);
            StartCoroutine(GotoNextLevel());
        }
    }

    public void Open()
    {
        animator.SetTrigger("Open");
        _open = true;
        StartCoroutine(PlayElevatorSounds(true));
    }

    private IEnumerator GotoNextLevel()
    {
        animator.SetTrigger("Close");
        Instantiate(elevatorDoor);
        yield return new WaitForSeconds(0.5f);
        if (stopTrackThisLevel)
        {
            GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>().StopTrack();
            GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>().enabled = false;
        }
        GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().PlayElevatorTransition();
    }
    
    private IEnumerator PlayElevatorSounds(bool openSound)
    {
        Instantiate(elevatorBeep);
        yield return new WaitForSeconds(0.1f);
        if (openSound)
            Instantiate(elevatorDoor);
    }
}
