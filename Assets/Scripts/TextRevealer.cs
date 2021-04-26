using System.Collections;
using TMPro;
using UnityEngine;

public class TextRevealer : MonoBehaviour
{
    [SerializeField] private float secondsPerLetter = 0.05f;

    [SerializeField] private bool animateRecord;
    [SerializeField] private GameObject sleeve;
    [SerializeField] private GameObject vinyl;
    
    private TextMeshProUGUI _uiText;
    private string _textToReveal;
    private bool _textDone;
    private bool _skipDialogue;
    
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
                _skipDialogue = true;
            }
            else if (animateRecord)
            {
                StartCoroutine(RevealRecord());
            }
        }
    }

    private IEnumerator BeginReveal()
    {
        _textDone = false;
        
        foreach (char character in _textToReveal)
        {
            _uiText.text += character;

            if (_skipDialogue)
            {
                break;
            }
            
            if (character != ' ')
            {
                // play sound
            }
            
            yield return new WaitForSeconds(secondsPerLetter);
        }

        _uiText.text = _textToReveal;
        _textDone = true;
    }

    private IEnumerator RevealRecord()
    {
        sleeve.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        vinyl.SetActive(true);
        yield return new WaitForSeconds(1.5f);
    }
}
