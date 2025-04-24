using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Invoke("LoadLevel", 2f);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene("LEVEL_2 1");
    }
}
