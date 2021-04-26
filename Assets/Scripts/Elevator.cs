using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private bool open;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && open)
        {
            Destroy(other.gameObject);
            StartCoroutine(GotoNextLevel());
        }
    }

    public void Open()
    {
        animator.SetTrigger("Open");
        open = true;
    }

    private IEnumerator GotoNextLevel()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().PlayElevatorTransition();
    }
}
