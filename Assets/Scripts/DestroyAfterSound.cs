using System.Collections;
using UnityEngine;

public class DestroyAfterSound : MonoBehaviour
{
    [SerializeField] private bool randomizePitch;
    
    private float seconds;

    private void Start()
    {
        seconds = GetComponent<AudioSource>().clip.length + 0.5f;
        StartCoroutine(TimeToDestroy());
        DontDestroyOnLoad(gameObject);

        if (randomizePitch)
            GetComponent<AudioSource>().pitch += Random.Range(-0.1f, 0.1f);
    }

    private IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(seconds);
        
        Destroy(gameObject);
    }
}
