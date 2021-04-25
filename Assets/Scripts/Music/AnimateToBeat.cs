using UnityEngine;

public class AnimateToBeat : MonoBehaviour
{
    [SerializeField] private Texture2D[] frames; 
    
    private Conductor _conductor;
    private int _currentAnimFrame = 0;
    private Material _mat;
    private bool _initialized;
    
    private void Update()
    {
        if (!_initialized)
        {
            _initialized = true;
            
            _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
            _conductor.beat.AddListener(AnimateFrameOnBeat);

            _mat = GetComponent<MeshRenderer>().material;
        }
    }
    
    private void AnimateFrameOnBeat()
    {
        _currentAnimFrame++;
        if (_currentAnimFrame == frames.Length)
        {
            _currentAnimFrame = 0;
        }
        
        _mat.SetTexture("_BaseMap", frames[_currentAnimFrame]);
    }
}
