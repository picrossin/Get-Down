using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject elevatorMusic;
    [SerializeField] private float currentVol;

    public float CurrentVol
    {
        get => currentVol;
        set => currentVol = value;
    }

    private GameObject _ui;

    private bool _playElevator = true;
    public bool PlayElevator => _playElevator;

    private void Start()
    {
        GameObject[] otherTransitionManagers = GameObject.FindGameObjectsWithTag("TransitionManager");
        if (otherTransitionManagers.Length > 1)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_ui == null) // on scene start
        {
            _ui = GameObject.FindGameObjectWithTag("UI");
            if (_ui != null)
            {
                if (_playElevator)
                {
                    StartCoroutine(MusicVolUp());
                    _ui.GetComponent<UIManager>().ElevatorTransition.SetActive(true);
                }
                else
                {
                    _ui.GetComponent<UIManager>().DeathTransition.SetActive(true);
                }
            }
        }
    }

    public void PlayDeathTransition()
    {
        _playElevator = false;
        _ui.GetComponent<UIManager>().DeathTransition.SetActive(true);
        _ui.GetComponent<UIManager>().DeathTransition.GetComponent<Animator>().SetTrigger("Die");
        StartCoroutine(WaitToLoadScene(SceneManager.GetActiveScene().buildIndex, 0.5f));
    }

    public void PlayElevatorTransition()
    {
        _playElevator = true;
        StartCoroutine(MusicVolDown());
        Instantiate(elevatorMusic);
        _ui.GetComponent<UIManager>().ElevatorTransition.SetActive(true);
        _ui.GetComponent<UIManager>().ElevatorTransition.GetComponent<Animator>().SetTrigger("FinishLevel");
        StartCoroutine(WaitToLoadScene(SceneManager.GetActiveScene().buildIndex + 1, 4f));
    }

    private IEnumerator WaitToLoadScene(int buildIndex, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync(buildIndex);
    }

    private IEnumerator MusicVolDown()
    {
        Conductor conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
        AudioSource conductorSource = conductor.gameObject.GetComponent<AudioSource>();
        currentVol = conductorSource.volume;

        while (conductorSource.volume > 0.1f)
        {
            conductorSource.volume -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    private IEnumerator MusicVolUp()
    {
        Conductor conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
        AudioSource conductorSource = conductor.gameObject.GetComponent<AudioSource>();

        while (conductorSource.volume < currentVol)
        {
            conductorSource.volume += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }

        conductorSource.volume = currentVol;
    }
}
