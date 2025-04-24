using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Sword Settings")]
    public static bool CanDamage = false;
    [SerializeField] public float DamageDefault = 1.0f;
    public float rayLength = 1.13f; // Adjustable length of the ray
    public LayerMask layerMask;

    void Update()
    {
        // Update the ray every frame with the current position and direction
        Ray ray = new Ray(transform.position, transform.up);
        CheckHit(ray);
    }

    void CheckHit(Ray ray)
    {
        if (CanDamage)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
            {
                //Debug.Log(hit.collider.gameObject.name + " was hit with damage ---> " + DamageDefault);

                //hit.collider.GetComponentInChildren<Enemy_1>().getDamage();

                //Deal Damage For Enemy Skeleton
                EnemySkeleton enemyComponent = hit.collider.GetComponentInChildren<EnemySkeleton>();

                if (enemyComponent != null)
                {
                    // Call the getDamage method on the component
                    enemyComponent.getDamage(DamageDefault);
                }


                //Deal Damage For Boss_0
                Boss_0 b0 = hit.collider.GetComponentInChildren<Boss_0>();

                if (b0 != null)
                {
                    // Call the getDamage method on the component
                    b0.getDamage(DamageDefault * b0.DPFPS);
                }

            }
        }

    }

    public void set_CanDamage_true()
    {
        //Debug.Log("Set Damage");
        CanDamage = true;
    }

    public void set_CanDamage_false()
    {
        //Debug.Log("Unset Damage");
        CanDamage = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draw the ray starting from the object's position in the upward direction with the adjustable length
        Gizmos.DrawRay(transform.position, transform.up * rayLength);
    }

}
