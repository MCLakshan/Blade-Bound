using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    void Start()
    {
        Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        
    }
}
