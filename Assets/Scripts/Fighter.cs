using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
{
    [Header("Level")]
    public int LEVEL = 0;


    [Header("Player Settings")]
    // [SerializeField] float cooldownTime = 0.8f;
    [SerializeField] public float MaxHealth = 1000f;
    [SerializeField] public float Health = 1000f;

    [Header("Sheild Settings")]
    public bool isShield = false;
    [SerializeField] float ShieldTime = 3f;
    [SerializeField] float ShieldCastedTime = 0;
    [SerializeField] float ShieldCooldownTime = 10f;
    public Slider SheildSlider;

    [Header("Heal Settings")]
    [SerializeField] GameObject heal = null;
    [SerializeField] private float HealAmount = 100f;
    public bool isHeal = false;
    [SerializeField] float HealTime = 1f;
    [SerializeField] float HealCooldownTime = 60f;
    public float HealCastedTime = 0;
    public Slider HealSlider = null;

    [Header("Player Attack Settings")]
    [SerializeField] GameObject weapon;
    [SerializeField] float maxComboDelay = 1.4f;
    [SerializeField] float singleAttackDelay = 0.8f;
    [SerializeField] float dodgeDelay = 1f;
    [SerializeField] float AttakTime = 0;
    [SerializeField] bool is_ComboAttak = false;
    [SerializeField] bool is_SingleAttak = false;
    public bool isGrounded;
    public bool isHit1 = false;
    public bool isHit2 = false;
    public bool isHit3 = false;
    public bool isDodge = false;

    [Header("Raycast Attack Settings")]
    [SerializeField] private float HitAmount_hit1 = 20f;
    [SerializeField] private float HitAmount_hit2 = 20f;
    [SerializeField] private float HitAmount_hit3 = 20f;
    [SerializeField] private float rayLength = 2f; // Adjustable length of the ray
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 rayOffset = new Vector3(0, 1.46f, 0); // Offset for ray

    [Header("Player Spell Attack Settings")]
    [SerializeField] public float spellDamageAmount = 50f;
    [SerializeField] private float spellDelay = 1.033f;
    [SerializeField] private float maxSpellCharge = 15f; // as a time cost
    [SerializeField] private float currentSpellCharge = 15f; // as a time cost
    [SerializeField] private float SpellCost = 5f; // as a time cost
    public bool isSpell = false;
    public Slider MagicSlider;

    [Header("Sword Glow and Trail")]
    [SerializeField] GameObject Trail = null;


    


    private Animator anim;
    private float nextFireTime = 0f;
    private float lastClickedTime = 0;
    private float lastSpellTime = 0;
    public static int noOfClicks = 0;
    private const int MaxClicks = 3;
    private static float ShieldSliderVal = 1f;
    private static float HealSliderVal = 1f;
    private static float MagicSliderVal = 1f;

    GameObject WeaponInHand;


    PlayerController playerController;
    PlayerSoundsController playerSoundsController;

    Sword sword;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerSoundsController = GetComponent<PlayerSoundsController>();
        sword = GetComponent<Sword>();

        WeaponInHand = weapon;

        GameObject shield = transform.Find("Shield").gameObject;
        shield.SetActive(false);

        if (LEVEL > 0)
        {
            //GameObject heal = transform.Find("Heal").gameObject;
            heal.SetActive(false);

            HealSlider.value = 1f;
            Trail.SetActive(false);
        }

        SheildSlider.value = 1f;
        MagicSlider.value = 1f;
    }

    void Update()
    {

        if (Health <= 0)
        {
            //Destroy(gameObject);
            //anim.SetBool("isHit", false);
            //anim.SetBool("isDie", true);
            Debug.Log("Player Died");
        }

        isGrounded = playerController.isGrounded;
        AttakTime = Time.time - lastClickedTime;

        // combo attack
        if (Time.time - lastClickedTime > maxComboDelay && is_ComboAttak)
        {
            ResetAll();
            playerController.canMove = true;
            if(LEVEL > 0)
            {
                Trail.SetActive(false);
            }
        }

        // single attack
        if (Time.time - lastClickedTime > singleAttackDelay && is_SingleAttak)
        {
            ResetAll();
            playerController.canMove = true ;
            if (LEVEL > 0)
            {
                Trail.SetActive(false);
            }
        }

        //Spell
        if (Time.time - lastSpellTime > spellDelay && isSpell)
        {
            anim.SetBool("spell", false);
            isSpell = false;
            playerController.canMove = true;

        }

        //Doge
        if (Time.time - lastClickedTime > dodgeDelay && isDodge)
        {
            ResetAll();
            playerController.canMove = true;
            isDodge = false;
            anim.SetBool("dodge", false);
            isHit3 = false;
        }


        if (Input.GetMouseButtonDown(0) && playerController.isGrounded)
        {
            if (playerController.get_is_Shift)
            {
                // Single attak
                SingleAttack();
                playerController.canMove = false;
                is_SingleAttak = true;
                if (LEVEL > 0)
                {
                    Trail.SetActive(true);
                }
            }
            else if (Time.time > nextFireTime)
            {
                //Combo attak
                OnClick();
                playerController.canMove = false;
                is_ComboAttak = true;
                if(LEVEL > 0)
                {
                    Trail.SetActive(true);
                }
            }

        }

        // dodge
        if (playerController.canMove && Input.GetKeyDown(KeyCode.LeftControl))
        {
            lastClickedTime = Time.time;
            isDodge = true;
            Debug.Log("Dodge");
            anim.SetBool("dodge", true);
            isHit3 = true;
            playerSoundsController.Sound_Dodge_WithDelay();
        }

        // spell attack
        if (playerController.canMove && Input.GetKeyDown(KeyCode.E) && !isSpell && currentSpellCharge >= SpellCost)
        {
            lastSpellTime = Time.time;
            isSpell = true;
            Debug.Log("Spell");
            anim.SetBool("spell", true);
            playerController.canMove = false;

            currentSpellCharge -= SpellCost;
            var proj_fireball = gameObject.GetComponentInChildren<projectile_fireball>();
            proj_fireball.FireMagicBall();
            
        }

        currentSpellCharge = Mathf.Clamp(currentSpellCharge+(Time.time - lastSpellTime), 0, maxSpellCharge);
        MagicSliderVal = Mathf.Clamp01((currentSpellCharge)/ maxSpellCharge);
        MagicSlider.value = MagicSliderVal;

        // Sheild
        if ((Time.time - ShieldCastedTime) > ShieldTime)
        {
            GameObject shield = transform.Find("Shield").gameObject;
            shield.SetActive(false);
            isShield = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && (Time.time - ShieldCastedTime) > ShieldCooldownTime)
        {
            isShield = true;
            ShieldCastedTime = Time.time;
            GameObject shield = transform.Find("Shield").gameObject;
            shield.SetActive(true); // Enable the shield
        }

        // Sheild slider

        float tempSheildTime = Time.time - ShieldCastedTime;
        float tempSheildTime1 =ShieldCastedTime - tempSheildTime;

        ShieldSliderVal = Mathf.Clamp01((ShieldCooldownTime - tempSheildTime)/ShieldCooldownTime);
        SheildSlider.value = ShieldSliderVal;

        if(LEVEL >  0)
        {
            // Heal 
            if ((Time.time - HealCastedTime) > HealTime)
            {
                //GameObject heal = transform.Find("Heal").gameObject;
                heal.SetActive(false);
                isHeal = false;
            }

            if (Input.GetKeyDown(KeyCode.H) && (Time.time - HealCastedTime) > HealCooldownTime && !isHeal)
            {
                isHeal = true;
                HealCastedTime = Time.time;
                //GameObject heal = transform.Find("Heal").gameObject;
                heal.SetActive(true); // Enable the shield

                // Healing the player
                Health += HealAmount;
                if(Health > MaxHealth)
                {
                    Health = MaxHealth;
                }

                var hb = gameObject.GetComponentInChildren<HealthBar>();
                if (hb != null)
                {
                    hb.setHealth(Health);
                }
            }

            // Heal slider

            float tempHealTime = Time.time - HealCastedTime;

            HealSliderVal = Mathf.Clamp01((HealCooldownTime - tempHealTime) / HealCooldownTime);
            HealSlider.value = HealSliderVal;
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks = Mathf.Clamp(noOfClicks + 1, 0, MaxClicks);

        if (isHit3)
        {
            ResetAll();
        }

        switch (noOfClicks)
        {
            case 1:
                anim.SetBool("hit_1", true);
                isHit1 = true;
                playerSoundsController.Sound_ComboHit1_WithDelay();
                break;
            case 2:
                anim.SetBool("hit_2", true);
                isHit2 = true;
                playerSoundsController.Sound_ComboHit2_WithDelay();
                break;
            case 3:
                anim.SetBool("hit_3", true);
                isHit3 = true;
                playerSoundsController.Sound_ComboHit3_WithDelay();
                break;
        }
    }

    void SingleAttack()
    {
        lastClickedTime = Time.time;
        anim.SetBool("hit_1", true);
        isHit1 = true;
        playerSoundsController.Sound_SingleAttack_WithDelay();
    }

    

    void SpellAttack()
    {
        lastClickedTime = Time.time;
        anim.SetBool("spell", true);
        isSpell = true;
    }

    private void ResetAll()
    {
        anim.SetBool("hit_1", false);
        anim.SetBool("hit_2", false);
        anim.SetBool("hit_3", false);
        isHit1 = false;
        isHit2 = false;
        isHit3 = false;
        is_ComboAttak = false;
        is_SingleAttak = false;
        //Debug.Log("noOfClicks = 0 ---> By ResetCombo");
        noOfClicks = 0;
    }


    public void StartDamage()
    {
        //WeaponInHand.GetComponentInChildren<Sword>().set_CanDamage_true();
    }

    public void StopDamage()
    {
        //WeaponInHand.GetComponentInChildren<Sword>().set_CanDamage_false();
    }

    public void RayHit_1()
    {
        Ray ray = new Ray(transform.position + rayOffset, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
        {
            //Deal Damage For Enemy Skeleton
            EnemySkeleton enemyComponent = hit.collider.GetComponentInChildren<EnemySkeleton>();
            EnemySkeletonwelcome enemyComponent1 = hit.collider.GetComponentInChildren<EnemySkeletonwelcome>();

            if (enemyComponent != null)
            {
                // Call the getDamage method on the component
                enemyComponent.getDamage(HitAmount_hit1);
            }
            else if (enemyComponent1 != null) 
            {
                enemyComponent1.getDamage(HitAmount_hit1);
            }


            //Deal Damage For Boss_0
            Boss_0 b0 = hit.collider.GetComponentInChildren<Boss_0>();

            if (b0 != null)
            {
                // Call the getDamage method on the component
                b0.getDamage(HitAmount_hit1 * b0.DPFPS);
            }

            //Deal Damage For Boss_1
            Boss_1 b1 = hit.collider.GetComponentInChildren<Boss_1>();

            if (b1 != null)
            {
                // Call the getDamage method on the component
                b1.getDamage(HitAmount_hit1 * b1.DPFPS);
            }

            //Deal Damage For Boss_2
            Boss_2 b2 = hit.collider.GetComponentInChildren<Boss_2>();

            if (b2 != null)
            {
                // Call the getDamage method on the component
                b2.getDamage(HitAmount_hit1 * b2.DPFPS);
            }
        }
    }

    public void RayHit_2()
    {
        Ray ray = new Ray(transform.position + rayOffset, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
        {
            Debug.Log("h2");

            //Deal Damage For Enemy Skeleton
            EnemySkeleton enemyComponent = hit.collider.GetComponentInChildren<EnemySkeleton>();
            EnemySkeletonwelcome enemyComponent1 = hit.collider.GetComponentInChildren<EnemySkeletonwelcome>();

            if (enemyComponent != null)
            {
                // Call the getDamage method on the component
                enemyComponent.getDamage(HitAmount_hit2);
            }
            else if (enemyComponent1 != null)
            {
                enemyComponent1.getDamage(HitAmount_hit1);
            }


            //Deal Damage For Boss_0
            Boss_0 b0 = hit.collider.GetComponentInChildren<Boss_0>();

            if (b0 != null)
            {
                // Call the getDamage method on the component
                b0.getDamage(HitAmount_hit2 * b0.DPFPS);
            }

            //Deal Damage For Boss_1
            Boss_1 b1 = hit.collider.GetComponentInChildren<Boss_1>();

            if (b1 != null)
            {
                // Call the getDamage method on the component
                b1.getDamage(HitAmount_hit1 * b1.DPFPS);
            }

            // Deal Damage For Boss_2
            Boss_2 b2 = hit.collider.GetComponentInChildren<Boss_2>();

            if (b2 != null)
            {
                // Call the getDamage method on the component
                b2.getDamage(HitAmount_hit2 * b2.DPFPS);
            }
        }
    }

    public void RayHit_3()
    {
        Ray ray = new Ray(transform.position + rayOffset, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
        {
            //Deal Damage For Enemy Skeleton
            EnemySkeleton enemyComponent = hit.collider.GetComponentInChildren<EnemySkeleton>();
            EnemySkeletonwelcome enemyComponent1 = hit.collider.GetComponentInChildren<EnemySkeletonwelcome>();

            if (enemyComponent != null)
            {
                // Call the getDamage method on the component
                enemyComponent.getDamage(HitAmount_hit3);
            }
            else if (enemyComponent1 != null)
            {
                enemyComponent1.getDamage(HitAmount_hit1);
            }


            //Deal Damage For Boss_0
            Boss_0 b0 = hit.collider.GetComponentInChildren<Boss_0>();

            if (b0 != null)
            {
                // Call the getDamage method on the component
                b0.getDamage(HitAmount_hit3 * b0.DPFPS);
            }

            //Deal Damage For Boss_1
            Boss_1 b1 = hit.collider.GetComponentInChildren<Boss_1>();

            if (b1 != null)
            {
                // Call the getDamage method on the component
                b1.getDamage(HitAmount_hit1 * b1.DPFPS);
            }

            // Deal Damage For Boss_2
            Boss_2 b2 = hit.collider.GetComponentInChildren<Boss_2>();

            if (b2 != null)
            {
                // Call the getDamage method on the component
                b2.getDamage(HitAmount_hit3 * b2.DPFPS);
            }
        }
    }

    public void getDamage(float damageAmount)
    { 
        if(!isShield)
        {
            Health -= damageAmount;
            var h_bar = gameObject.GetComponentInChildren<HealthBar>();
            if (h_bar != null)
            {
                h_bar.takeDamage(damageAmount);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + rayOffset, transform.forward * rayLength); // Apply the offset here
    }
}
