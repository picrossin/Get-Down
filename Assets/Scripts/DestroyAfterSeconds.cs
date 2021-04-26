using System.Collections;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float secondsBeforeDestroy = 5f;

    private void Start()
    {
        StartCoroutine(DestroyAfter());
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(secondsBeforeDestroy);
        Destroy(gameObject);
    }
}
