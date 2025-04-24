using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer_EG2 : MonoBehaviour
{
    [SerializeField] GameObject ESK1 = null;
    [SerializeField] GameObject ESK2 = null;
    [SerializeField] GameObject ESK3 = null;
    [SerializeField] GameObject ESK4 = null;
    [SerializeField] GameObject ESK5 = null;
    [SerializeField] GameObject ESK6 = null;
    [SerializeField] GameObject ESK7 = null;
    [SerializeField] GameObject ESK8 = null;
    [SerializeField] GameObject ESK9 = null;
    [SerializeField] GameObject ESK10 = null;
    [SerializeField] GameObject ESK11 = null;

    private void OnTriggerEnter(Collider other)
    {
        Esk_set_T_lock(ESK1);
        Esk_set_T_lock(ESK2);
        Esk_set_T_lock(ESK3);
        Esk_set_T_lock(ESK4);
        Esk_set_T_lock(ESK5);
        Esk_set_T_lock(ESK6);
        Esk_set_T_lock(ESK7);
        Esk_set_T_lock(ESK8);
        Esk_set_T_lock(ESK9);
        Esk_set_T_lock(ESK10);
        Esk_set_T_lock(ESK11);
    }

    private void Esk_set_T_lock(GameObject esk)
    {
        if (esk != null)
        {
            EnemySkeleton e = esk.GetComponentInChildren<EnemySkeleton>();
            if (e != null)
            {
                e.set_T_lock();
            }
        }
    }
}
