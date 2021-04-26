using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private GameObject _ui;

    private bool _playElevator = true;
    
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
        _ui.GetComponent<UIManager>().ElevatorTransition.SetActive(true);
        _ui.GetComponent<UIManager>().ElevatorTransition.GetComponent<Animator>().SetTrigger("FinishLevel");
        StartCoroutine(WaitToLoadScene(SceneManager.GetActiveScene().buildIndex + 1, 4f));
    }

    private IEnumerator WaitToLoadScene(int buildIndex, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync(buildIndex);
    }
}
