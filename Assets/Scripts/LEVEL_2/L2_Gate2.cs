using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_Gate2 : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject HB;
    [SerializeField] GameObject G1_ESK1;
    [SerializeField] GameObject G1_ESK2;
    [SerializeField] GameObject G1_ESK3;
    [SerializeField] GameObject G1_ESK4;

    //Enemy Group 2
    [SerializeField] GameObject G2_ESK1;
    [SerializeField] GameObject G2_ESK2;
    [SerializeField] GameObject G2_ESK3;
    [SerializeField] GameObject G2_ESK4;
    [SerializeField] GameObject G2_ESK5;

    //Final Area
    [SerializeField] GameObject FinalArea;

    private void Start()
    {
        FinalArea.SetActive(false);
    }

    void Update()
    {
        if(CheckHealthBoss(Boss) && CheckHealth(G1_ESK1) &&
            CheckHealth(G1_ESK2) && CheckHealth(G1_ESK3) && 
            CheckHealth(G1_ESK4) && CheckHealth(G2_ESK1) && 
            CheckHealth(G2_ESK2) && CheckHealth(G2_ESK3) && 
            CheckHealth(G2_ESK4) && CheckHealth(G2_ESK5))
        {
            FinalArea.SetActive(true);
            Destroy(HB);
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

    private bool CheckHealthBoss(GameObject g)
    {
        if (g != null)
        {
            Boss_2 b = g.GetComponent<Boss_2>();
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
