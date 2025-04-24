using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final_0 : MonoBehaviour
{
    [SerializeField] GameObject SpawnEffect = null;

    private void Start()
    {
        if (SpawnEffect != null)
        {
            SpawnEffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SpawnEffect != null)
        {
            SpawnEffect.SetActive(true);
        }

        Invoke("LoadLevel1", 2f);
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene("LEVEL_1");
    }


}
