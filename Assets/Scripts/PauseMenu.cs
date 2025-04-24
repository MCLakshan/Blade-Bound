using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject PauseMenuUI;

    private List<GameObject> previouslyDisabledCanvases = new List<GameObject>();

    private void Start()
    {

        //Debug.Log("CS --->" + SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name != "LEVEL_0")
        {
            Resume();

        }
        else
        {
            Resume_L0();
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        foreach (GameObject canvas in previouslyDisabledCanvases)
        {
            canvas.SetActive(true);
        }

        previouslyDisabledCanvases.Clear();
    }

    public void Resume_L0()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        

        foreach (GameObject canvas in previouslyDisabledCanvases)
        {
            canvas.SetActive(true);
        }

        previouslyDisabledCanvases.Clear();
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Canvas[] allCanvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvases)
        {
            if (canvas.gameObject.name != "Canvas_Pause_Menu")
            {
                canvas.gameObject.SetActive(false);
                previouslyDisabledCanvases.Add(canvas.gameObject);
            }
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MENU");
    }

    public void Quit()
    {
        Application.Quit();

    }
}
