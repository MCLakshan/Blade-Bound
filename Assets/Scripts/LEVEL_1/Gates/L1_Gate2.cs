using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_Gate2 : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject HB;

    [SerializeField] GameObject ESK1;
    [SerializeField] GameObject ESK2;
    [SerializeField] GameObject ESK3;
    [SerializeField] GameObject ESK4;
    [SerializeField] GameObject ESK5;
    [SerializeField] GameObject ESK6;
    [SerializeField] GameObject ESK7;

    private void Update()
    {
        if (CheckHealth(ESK1) && CheckHealth(ESK2) && CheckHealth(ESK3) &&
            CheckHealth(ESK4) && CheckHealth(ESK5) && CheckHealth(ESK6) &&
            CheckHealth(ESK7) && CheckBossHealth(Boss))
        {
            Destroy(gameObject);
            Destroy(HB);
        }

    }

    private bool CheckHealth(GameObject g)
    {
        if (g != null)
        {
            EnemySkeleton e = g.GetComponentInChildren<EnemySkeleton>();
            if (e.getHealth() <= 0)
            {
                return true;
            }
        }
        else
        {
            return true;
        }
        return false;
    }

    private bool CheckBossHealth(GameObject g)
    {
        if (g != null)
        {
            Boss_1 b = g.GetComponentInChildren<Boss_1>();
            if (b.getHealth() <= 0)
            {
                return true;
            }
        }
        else
        {
            return true;
        }
        return false;
    }
}
