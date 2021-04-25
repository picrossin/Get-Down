using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Elevator elevator;
    
    private int _enemyCount = Int32.MaxValue;
    
    private void Start()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyKilled()
    {
        _enemyCount--;
        
        if (_enemyCount <= 0)
        {
            elevator.Open();
        }
    }
}
