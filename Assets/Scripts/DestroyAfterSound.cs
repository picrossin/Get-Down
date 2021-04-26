using System.Collections;
using UnityEngine;

public class DestroyAfterSound : MonoBehaviour
{
    private float seconds;

    private void Start()
    {
        seconds = GetComponent<AudioSource>().clip.length + 0.5f;
        StartCoroutine(TimeToDestroy());
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(seconds);
        
        Destroy(gameObject);
    }
}
