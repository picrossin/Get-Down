using UnityEngine;
using UnityEngine.UI;

public class RotateMouseSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int framesBetweenSwitch = 90;

    private Image _image;
    private int _index;
    private int _frameCounter;

    private void Start()
    {
        _image = GetComponent<Image>();
    }
    
    private void FixedUpdate()
    {
        _frameCounter++;
        if (_frameCounter >= framesBetweenSwitch)
        {
            _frameCounter = 0;
            _index++;
            if (_index == sprites.Length)
            {
                _index = 0;
            }

            _image.sprite = sprites[_index];
        }
    }
}
