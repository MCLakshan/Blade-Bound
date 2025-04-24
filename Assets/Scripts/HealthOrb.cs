using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    [Header("Health Orb Settings")]
    [SerializeField] private float Health;
    [SerializeField] private GameObject Player;
    [SerializeField] private LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {
            //Debug.Log("Healing Player");

            Fighter fighter = other.gameObject.GetComponent<Fighter>();
            if (fighter != null)
            {
                if (fighter.MaxHealth <= fighter.Health + Health)
                {
                    fighter.Health = fighter.MaxHealth;
                    fighter.GetComponentInChildren<HealthBar>().setHealth(fighter.MaxHealth);
                }
                else
                {
                    fighter.Health += Health;
                    fighter.GetComponentInChildren<HealthBar>().heal(Health);
                }
                Debug.Log("Player Healed By +" + Health );
                Destroy(gameObject);
            }

        }

        //Debug.Log("Healing " + other.gameObject.layer);
    }
}
