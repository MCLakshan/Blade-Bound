using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer_EG3 : MonoBehaviour
{
    [SerializeField] GameObject Boss1 = null;
    [SerializeField] GameObject Boss2 = null;

    [SerializeField] GameObject ESK = null;
    [SerializeField] GameObject ESK2 = null;
    [SerializeField] GameObject ESK3 = null;
    [SerializeField] GameObject ESK4 = null;

    [SerializeField] GameObject Wall1 = null;
    [SerializeField] GameObject Wall2 = null;

    [SerializeField] GameObject HelthBars = null;

    private void Start()
    {
        Wall1.SetActive(false);
        Wall2.SetActive(false);

        HelthBars.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Boss_set_CanMove(Boss1);
        Boss_set_CanMove(Boss2);

        Esk_set_T_lock(ESK);
        Esk_set_T_lock(ESK2);
        Esk_set_T_lock(ESK3);
        Esk_set_T_lock(ESK4);

        Wall1.SetActive(true);
        Wall2.SetActive(true);

        HelthBars.SetActive(true);

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
        Boss_0 b = B.GetComponent<Boss_0>();
        if (b != null)
        {
            b.SeePlayer();
        }
    }
}
