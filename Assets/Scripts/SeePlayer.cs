using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer : MonoBehaviour
{
    [SerializeField] GameObject ESK1 = null;
    [SerializeField] GameObject ESK2 = null;

    private void OnTriggerEnter(Collider other)
    {
        if (ESK1 != null)
        {
            EnemySkeleton e = ESK1.GetComponentInChildren<EnemySkeleton>();
            if(e != null)
            {
                e.set_T_lock();
            }
        }

        if (ESK2 != null)
        {
            EnemySkeleton e = ESK2.GetComponentInChildren<EnemySkeleton>();
            if (e != null)
            {
                e.set_T_lock();
            }
        }
    }
}
