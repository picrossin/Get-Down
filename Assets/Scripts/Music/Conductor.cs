using UnityEngine;
using UnityEngine.Events;

// INSPIRED BY "Coding to the Beat - Under the Hood of a Rhythm Game in Unity" by Graham Tattersal

[RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    public UnityEvent beat = new UnityEvent();

    private bool _currentTrackStarted;
    public bool CurrentTrackStarted => _currentTrackStarted;

    private float _beatInMeasure;
    public float BeatInMeasure => _beatInMeasure;
    
    private float songBpm;

    private AudioSource musicSource;
    
    private float _secPerBeat;

    private float _songPosition;

    public float _songPositionInBeats;

    private float _dspSongTime; // seconds passed since the song started

    private float _lastBeat;

    private void Start()
    {
        GameObject[] otherConductors = GameObject.FindGameObjectsWithTag("Conductor");
        if (otherConductors.Length > 1)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        musicSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Determine seconds since song started
        _songPosition = (float) (AudioSettings.dspTime - _dspSongTime);
        
        // Determine beats since the song started
        _songPositionInBeats = _songPosition / _secPerBeat;
        
        // Determine beat in measure
        _beatInMeasure = _songPositionInBeats % 4;
        
        // Determine if the frame is on the beat
        if (_songPosition > _lastBeat + _secPerBeat)
        {
            beat.Invoke();
            _lastBeat += _secPerBeat;
        }
    }

    public void StopTrack()
    {
        _currentTrackStarted = false;
        musicSource.Stop();
    }
    
    public void StartTrack(AudioClip clip, float bpm)
    {
        musicSource.clip = clip;
        songBpm = bpm;
        SetupTrack();
        musicSource.Play();
        _currentTrackStarted = true;
    }

    private void SetupTrack()
    {
        _secPerBeat = 60f / songBpm;
        _lastBeat = 0;
        _dspSongTime = (float) AudioSettings.dspTime;
    }
}
