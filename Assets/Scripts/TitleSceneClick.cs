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
    [SerializeField] private GameObject hoverNoise;
    
    private Vector3 _initialPosition;
    private bool _inBox;
    private bool _clicked;
    
    private void Start()
    {
        _initialPosition = image.localPosition;
        fallingImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_inBox && !_clicked)
        {
            _clicked = true;
            StartCoroutine(StartGame());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.localPosition = _initialPosition + new Vector3(0, 10f, 0);
        _inBox = true;
        Instantiate(hoverNoise);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.localPosition = _initialPosition;
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
