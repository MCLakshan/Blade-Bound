using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer_EG2_L1 : MonoBehaviour
{
    [SerializeField] GameObject BossHB = null;

    [SerializeField] GameObject Boss = null;
    [SerializeField] GameObject ESK1 = null;
    [SerializeField] GameObject ESK2 = null;
    [SerializeField] GameObject ESK3 = null;
    [SerializeField] GameObject ESK4 = null;
    [SerializeField] GameObject ESK5 = null;
    [SerializeField] GameObject ESK6 = null;
    [SerializeField] GameObject ESK7 = null;

    [SerializeField] GameObject Boundary_Gate = null;

    private void Start()
    {
        BossHB.SetActive(false);
        Boundary_Gate.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        BossHB.SetActive(true);

        Boss_set_CanMove(Boss);
        Esk_set_T_lock(ESK1);
        Esk_set_T_lock(ESK2);
        Esk_set_T_lock(ESK3);
        Esk_set_T_lock(ESK4);
        Esk_set_T_lock(ESK5);
        Esk_set_T_lock(ESK6);
        Esk_set_T_lock(ESK7);

        Destroy(gameObject);
        Boundary_Gate.SetActive(true);

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

    private void Boss_set_CanMove(GameObject B)
    {
        Boss_1 b = B.GetComponent<Boss_1>();
        if (b != null)
        {
            b.SeePlayer();
        }
    }
}
