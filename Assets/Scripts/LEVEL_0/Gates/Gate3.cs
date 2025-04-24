using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate3 : MonoBehaviour
{
    [SerializeField] GameObject ESK1;
    [SerializeField] GameObject ESK2;
    [SerializeField] GameObject ESK3;
    [SerializeField] GameObject ESK4;
    [SerializeField] GameObject ESK5;

    private void Update()
    {
        if (CheckHealth(ESK1) && CheckHealth(ESK2) && CheckHealth(ESK3) &&
            CheckHealth(ESK4) && CheckHealth(ESK5))
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
