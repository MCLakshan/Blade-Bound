using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalTrigger : MonoBehaviour
{
    [SerializeField] GameObject camera_obj;

    private void OnTriggerEnter(Collider other)
    {       
        camera_obj.GetComponent<CameraFade>().FadeIn();
        Invoke("LoadEnd", 2f);
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene(4);
    }
}
