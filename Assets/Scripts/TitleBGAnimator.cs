using UnityEngine;
using UnityEngine.UI;

public class TitleBGAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] bgFrames;
    [SerializeField] private Image bgImage;

    [SerializeField] private AudioClip music;
    [SerializeField] private float bpm;
    
    private Conductor _conductor;
    private bool _initialized;
    private int _currentFrame;
    
    private void Update()
    {
        if (!_initialized)
        {
            _initialized = true;
            _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
            _conductor.beat.AddListener(UpdateBG);
            
            _conductor.StartTrack(music, bpm);
        }
    }

    private void UpdateBG()
    {
        _currentFrame++;
        if (_currentFrame == bgFrames.Length)
        {
            _currentFrame = 0;
        }

        bgImage.sprite = bgFrames[_currentFrame];
    }
}
