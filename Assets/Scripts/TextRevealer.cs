using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextRevealer : MonoBehaviour
{
    [SerializeField] private float secondsPerLetter = 0.05f;
    [SerializeField] private float secondsBetweenLetters = 0.025f;
    [SerializeField] private Image face;
    [SerializeField] private Sprite face1;
    [SerializeField] private Sprite face2;
    [SerializeField] private Animator transitionAnimator;
    
    [SerializeField] private bool animateRecord;
    [SerializeField] private GameObject sleeve;
    [SerializeField] private GameObject vinyl;
    [SerializeField] private GameObject voiceSound;
    [SerializeField] private GameObject selectSound;
    [SerializeField] private GameObject recordGetSound;
    [SerializeField] private bool lastScene;

    private TextMeshProUGUI _uiText;
    private string _textToReveal;
    private bool _textDone;
    private bool _skipDialogue;
    private bool _doneClicking;
    
    private void Start()
    {
        _uiText = GetComponent<TextMeshProUGUI>();
        _textToReveal = _uiText.text;
        _uiText.text = "";
        StartCoroutine(BeginReveal());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_textDone)
            {
                // _skipDialogue = true;
                // Instantiate(selectSound);
            }
            else if (animateRecord && !_doneClicking)
            {
                _doneClicking = true;
                StartCoroutine(RevealRecord());
                Instantiate(selectSound);
            }
            else
            {
                StartCoroutine(NextLevel());
            }
        }
    }

    private IEnumerator BeginReveal()
    {
        _textDone = false;
        
        yield return new WaitForSeconds(0.5f);

        int soundEveryChar = 4;
        int soundEveryCharIndex = 0;
        
        foreach (char character in _textToReveal)
        {
            _uiText.text += character;
            soundEveryCharIndex++;

            // if (_skipDialogue)
            // {
            //     break;
            // }
            
            if (character != ' ' && soundEveryCharIndex >= soundEveryChar)
            {
                soundEveryCharIndex = 0;
                Instantiate(voiceSound);
                face.sprite = face2;
            }
            
            yield return new WaitForSeconds(secondsPerLetter);

            face.sprite = face1;
            
            yield return new WaitForSeconds(secondsBetweenLetters);
        }

        face.sprite = face1;
        _uiText.text = _textToReveal;
        _textDone = true;
    }

    private IEnumerator RevealRecord()
    {
        sleeve.SetActive(true);
        Instantiate(recordGetSound);
        yield return new WaitForSeconds(1.2f);
        vinyl.SetActive(true);
    }

    private IEnumerator NextLevel()
    {
        transitionAnimator.SetTrigger("FinishLevel");
        yield return new WaitForSeconds(0.5f);
        if (lastScene)
        {
            Destroy(GameObject.FindGameObjectWithTag("Conductor"));
            GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().CurrentVol = 0.34f;
            Cursor.visible = true;
        }
        SceneManager.LoadSceneAsync(lastScene ? 0 : SceneManager.GetActiveScene().buildIndex + 1);
    }
}
