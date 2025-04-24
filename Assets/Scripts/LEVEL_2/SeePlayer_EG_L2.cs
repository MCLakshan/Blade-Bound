using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer_EG_L2 : MonoBehaviour
{
    [SerializeField] GameObject BossHB = null;

    [SerializeField] GameObject Boss = null;
    [SerializeField] GameObject ESK1 = null;
    [SerializeField] GameObject ESK2 = null;
    [SerializeField] GameObject ESK3 = null;
    [SerializeField] GameObject ESK4 = null;

    void Start()
    {
        BossHB.SetActive(false);

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BossHB.SetActive(true);

        Boss_set_CanMove(Boss);
        Esk_set_T_lock(ESK1);
        Esk_set_T_lock(ESK2);
        Esk_set_T_lock(ESK3);
        Esk_set_T_lock(ESK4);

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

    private void Boss_set_CanMove(GameObject B)
    {
        Boss_2 b = B.GetComponent<Boss_2>();
        if (b != null)
        {
            b.SeePlayer();
        }
    }
}
