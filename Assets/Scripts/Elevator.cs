using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject mesh;

    private bool open;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && open)
        {
            GotoNextLevel();
        }
    }

    public void Open()
    {
        mesh.SetActive(true);
        open = true;
    }

    private void GotoNextLevel()
    {
        GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().PlayElevatorTransition();
    }
}
