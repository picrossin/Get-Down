using System.Collections;
using UnityEngine;

public class FirstLevelSpriteSwap : MonoBehaviour
{
    [SerializeField] private GameObject fallingImage;
    [SerializeField] private GameObject elevatorImage;
    
    private void OnEnable()
    {
        fallingImage.SetActive(true);
        elevatorImage.SetActive(false);
        StartCoroutine(SwapSprites());
    }

    private IEnumerator SwapSprites()
    {
        yield return new WaitForSeconds(5f);
        fallingImage.SetActive(false);
        elevatorImage.SetActive(true);
    }
}
