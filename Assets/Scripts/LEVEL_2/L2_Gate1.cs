using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_Gate1 : MonoBehaviour
{
    [SerializeField] GameObject ESK1;
    [SerializeField] GameObject ESK2;
    [SerializeField] GameObject ESK3;

    void Update()
    {
        if (CheckHealth(ESK1) && CheckHealth(ESK2) && CheckHealth(ESK3))
        {
            Destroy(gameObject);
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
}
