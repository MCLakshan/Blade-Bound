using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 1000; 
    public float health = 1000;
    public float learpSpeed = 0.05f;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    takeDamage(100);
        //}
        
        if(healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, learpSpeed);
        }
    }

    public void takeDamage(float Damage)
    {
        health -= Damage;
    }

    public void heal(float amount)
    {
        health += amount;
    }

    public void setHealth(float amount) {
        health = amount;
    }
}
