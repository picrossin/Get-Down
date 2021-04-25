using UnityEngine;

public class BulletDetectRecords : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Record"))
        {
            transform.parent.GetComponent<EnemyBullet>().DestroyBullet();
        }
    }
}
