using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Trigger : MonoBehaviour
{
    [SerializeField] private GameObject Object;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Intro_Trigger_1");
        Object.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Destroy(gameObject);

    }
}
