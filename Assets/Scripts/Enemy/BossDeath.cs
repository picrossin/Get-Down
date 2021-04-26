using UnityEngine;

public class BossDeath : MonoBehaviour
{
    private Conductor _conductor;

    private void Start()
    {
        _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
        _conductor.GetComponent<AudioSource>().volume = 0;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        if (enemies.Length > 0)
        {
            GameObject.FindGameObjectWithTag("Elevator").GetComponent<Elevator>().Open();
        }
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        foreach (var bullet in enemyBullets)
        {
            Destroy(bullet);
        }
    }
}