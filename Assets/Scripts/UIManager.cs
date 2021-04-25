using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject elevatorTransition;
    public GameObject ElevatorTransition => elevatorTransition;
    
    [SerializeField] private GameObject deathTransition;
    public GameObject DeathTransition => deathTransition;
}
