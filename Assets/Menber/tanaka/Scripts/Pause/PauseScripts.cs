using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScripts : MonoBehaviour
{
    public bool pauseActive = false;
    [SerializeField]
    private GameObject pauseCanvas = null;
    void Start()
    {
    }
    void Update()
    {
        
        if(Time.timeScale != 0)
        {
            if (!pauseActive && Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("pause");
                StartCoroutine("PauseNow");
                pauseCanvas.gameObject.SetActive(true);
                GameManager.stopInputKey = true;
            }
        }
        
        if (pauseActive)
        {
            if (Input.GetButtonDown("DS4cross") || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LoadTitle();
            }
            if (Input.GetButtonDown("DS4circle") || Input.GetKeyDown(KeyCode.RightArrow))
            {
                EndGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine("PauseNow");
                pauseCanvas.gameObject.SetActive(false);
                GameManager.stopInputKey = false;
            }
        }
    }

    private IEnumerator PauseNow()
    {
        if (pauseActive)
        {
            Time.timeScale = 1.0f;
            yield return new WaitForSeconds(0.5f);
            pauseActive = false;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            pauseActive = true;
            Time.timeScale = 0.0f;
        }
        
    }

    public void LoadTitle()
    {
        pauseActive = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
    }
    public void EndGame()
    {
        pauseActive = false;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE_WIN
        Application.Quit();
#endif
    }
}
