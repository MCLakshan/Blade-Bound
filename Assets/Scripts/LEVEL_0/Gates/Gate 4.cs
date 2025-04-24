using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate4 : MonoBehaviour
{
    [SerializeField] GameObject Boss1;
    [SerializeField] GameObject Boss2;

    [SerializeField] GameObject ESK1;
    [SerializeField] GameObject ESK2;
    [SerializeField] GameObject ESK3;
    [SerializeField] GameObject ESK4;

    [SerializeField] GameObject Wall1;
    [SerializeField] GameObject Wall2;

    private void Update()
    {
        if (CheckHealth_E(ESK1) && CheckHealth_E(ESK2) && CheckHealth_E(ESK3) &&
            CheckHealth_E(ESK4) && CheckHealth_B(Boss1) && CheckHealth_B(Boss2))
        {
            Wall1.SetActive(false);
            Wall2.SetActive(false);
            Destroy(gameObject);
        }

    }

    private bool CheckHealth_E(GameObject g)
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

    private bool CheckHealth_B(GameObject g)
    {
        if (g != null)
        {
            Boss_0 b = g.GetComponent<Boss_0>();
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
