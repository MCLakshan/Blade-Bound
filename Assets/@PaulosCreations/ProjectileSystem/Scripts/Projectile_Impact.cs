using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Paulos.Projectiles
{
    public class Projectile_Impact : MonoBehaviour, IProjectileDamageable
    {
        [Header("Called when a Projectile hits this target.")]
        public ImpactEvent OnProjectileImpact;//passes the attacker and damage amount when invoked.
        [SerializeField] float ProjectileDamege = 100f;

        //Implementation of the IProjectileDamageable Interface
        public void OnProjectileDamage(Transform _attacker, float _damageAmount )
        {
            //OnProjectileImpact?.Invoke(_attacker, _damageAmount);

            //Deal Damage For Enemy Skeleton
            EnemySkeleton enemyComponent = this.gameObject.GetComponent<EnemySkeleton>();

            if (enemyComponent != null)
            {
                // Call the getDamage method on the component
                enemyComponent.getDamage(ProjectileDamege);
            }


            //Deal Damage For Boss_0
            Boss_0 b0 = this.gameObject.GetComponent<Boss_0>();

            if (b0 != null)
            {
                // Call the getDamage method on the component
                b0.getDamage(ProjectileDamege);
            }

            //Deal Damage For Boss_1
            Boss_1 b1 = this.gameObject.GetComponent<Boss_1>();

            if (b1 != null)
            {
                // Call the getDamage method on the component
                b1.getDamage(ProjectileDamege);
            }

            //Deal Damage For Boss_2
            Boss_2 b2 = this.gameObject.GetComponent<Boss_2>();

            if (b2 != null)
            {
                // Call the getDamage method on the component
                b2.getDamage(ProjectileDamege);
            }

        }
    }

    //custom event
    [System.Serializable]
    public class ImpactEvent : UnityEvent<Transform, float> { }

    //Custom Interface
    public interface IProjectileDamageable
    {
        void OnProjectileDamage(Transform _attacker, float _damageAmount);
    }
}
