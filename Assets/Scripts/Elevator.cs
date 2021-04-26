using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool stopTrackThisLevel;
    
    private bool _open;
    
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
    }

    private IEnumerator GotoNextLevel()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        if (stopTrackThisLevel)
        {
            GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>().StopTrack();
            GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>().enabled = false;
        }
        GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().PlayElevatorTransition();
    }
}
