using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleSceneClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private RectTransform image;
    [SerializeField] private GameObject fallingImage;
    [SerializeField] private Animator transitionAnimation;
    [SerializeField] private GameObject startGameSound;
    
    private Vector3 _initialPosition;
    private bool _inBox;
    
    private void Start()
    {
        _initialPosition = image.position;
        fallingImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_inBox)
        {
            StartCoroutine(StartGame());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.position = _initialPosition + new Vector3(0, 10f, 0);
        _inBox = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.position = _initialPosition;
        _inBox = false;
    }

    private IEnumerator StartGame()
    {
        fallingImage.SetActive(true);
        transitionAnimation.SetTrigger("FinishLevel");
        Instantiate(startGameSound);
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>().StopTrack();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
