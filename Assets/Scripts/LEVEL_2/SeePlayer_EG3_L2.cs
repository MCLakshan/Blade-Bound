using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer_EG3_L2 : MonoBehaviour
{
    [SerializeField] GameObject ESK1 = null;
    [SerializeField] GameObject ESK2 = null;
    [SerializeField] GameObject ESK3 = null;
    [SerializeField] GameObject ESK4 = null;
    [SerializeField] GameObject ESK5 = null;


    private void OnTriggerEnter(Collider other)
    {
        Esk_set_T_lock(ESK1);
        Esk_set_T_lock(ESK2);
        Esk_set_T_lock(ESK3);
        Esk_set_T_lock(ESK4);
        Esk_set_T_lock(ESK5);

        Destroy(gameObject);
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
