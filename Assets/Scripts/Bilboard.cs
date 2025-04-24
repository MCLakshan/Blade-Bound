using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    public Transform camara;

    void Update()
    {
        transform.LookAt(transform.position + camara.forward);
    }
}
