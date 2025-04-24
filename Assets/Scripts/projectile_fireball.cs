using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paulos.Projectiles;

public class projectile_fireball : MonoBehaviour
{
    [SerializeField] float delay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    FireMagicBall();
        //}
        
    }

    private IEnumerator FireWithDelay()
    {
        
        yield return new WaitForSeconds(delay);
        Projectile_Manager._Instance.FireProjectileForward("Projectile_Fire", transform);
    }

    public void FireMagicBall()
    {
        StartCoroutine(FireWithDelay());
    }
}
