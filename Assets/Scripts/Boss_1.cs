using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Boss_1 : MonoBehaviour
{
    [Header("Boss 01 Settings")]
    //[SerializeField] private bool isIdle = true;
    [SerializeField] private float health = 2000f;
    [SerializeField] private bool isSeePlayer = false;
    [SerializeField] private float isSeePlayerDelay = 5.4f;
    [SerializeField] private bool isChasePlayer = false;
    [SerializeField] private bool isDie = false;
    [SerializeField] private float lastAnimTime = 0f;  
    [SerializeField] private bool canMove = false;
    public NavMeshAgent boss_1;
    public Transform target;
    private GameObject playerGameObject;

    [Header("Attack 1 Setings")]
    [SerializeField] private float Attack1Damage = 50f;
    [SerializeField] private bool isAttack1 = false;
    [SerializeField] private float isAttack1Delay = 2.7f;

    [Header("Attack 2 Setings")]
    [SerializeField] private float Attack2Damage = 100f;
    [SerializeField] private bool isAttack2 = false;
    [SerializeField] private float isAttack2Delay = 3f;
    [SerializeField] private float Attack2_moveDuration = 1f;
    [SerializeField] private float Attack2_moveDistance = 1f;
    [SerializeField] private float Attack2CooldownTime = 10f;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLength = 2.5f; // Adjustable length of the ray
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 rayOffset1 = new Vector3(0, 1.46f, 0); // Offset for ray
    [SerializeField] private Vector3 rayOffset2 = new Vector3(0, 1.46f, 0); // Offset for ray
    [SerializeField] private Vector3 rayOffset3 = new Vector3(0, 1.46f, 0); // Offset for ray
    [SerializeField] private float rayLength4 = 4f; // Adjustable length of the ray
    [SerializeField] private Vector3 rayOffset4 = new Vector3(0, 1.46f, 0); // Offset for ray

    [Header("Damage Presentage From Player Sword")]
    [SerializeField] public float DPFPS = 1f;

    [Header("HelthBar")]
    [SerializeField] GameObject HB;

    [Header("Sound Settings")]
    [SerializeField] private AudioSource SoundRoar;
    [SerializeField] private float SoundRoarDelay = 0f;
    [SerializeField] private float SoundRoarPitch = 1.2f;

    [SerializeField] private AudioSource SoundWalk;
    [SerializeField] private float SoundWalkPitch = 1f;

    [SerializeField] private AudioSource SoundAttack1;
    [SerializeField] private float SoundAttack1Delay = 0f;

    [SerializeField] private AudioSource SoundAttack2;
    [SerializeField] private float SoundAttack2Delay = 0f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }

        playerGameObject = target.gameObject;

        canMove = false;
    }

    void Update()
    {
        SoundWalk.pitch = SoundWalkPitch;

        // See Player
        if (Time.time - lastAnimTime > isSeePlayerDelay && isSeePlayer)
        {
            anim.SetBool("seePlayer", false);
            isSeePlayer = false;
            canMove = true;
        }

        //Attack 1
        if (Time.time - lastAnimTime > isAttack1Delay && isAttack1)
        {
            anim.SetBool("attack1", false);
            isAttack1 = false;
            canMove = true;
        }

        // Attack 2
        if (Time.time - lastAnimTime > isAttack2Delay && isAttack2)
        {
            anim.SetBool("attack2", false);
            isAttack2 = false;
            canMove = true;
        }

        if (canMove && Input.GetKeyDown(KeyCode.Alpha1))
        {
            canMove = false;
            lastAnimTime = Time.time;
            isSeePlayer = true;
            anim.SetBool("seePlayer", true);
            Debug.Log("Boss_1 See Player.");
        }

        //Walk
        if (canMove)
        {
            boss_1.isStopped = false;
            boss_1.SetDestination(target.position);
            isChasePlayer = true;
            anim.SetBool("chase", true);
            SoundWalk.enabled = true;
        }
        else
        {
            boss_1.isStopped = true;
            isChasePlayer = false;
            anim.SetBool("chase", false);
            SoundWalk.enabled = false;

        }

        // Attack 1
        //if (canMove && Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    canMove = false;
        //    lastAnimTime = Time.time;
        //    isAttack1 = true;
        //    anim.SetBool("attack1", true);
        //}

        Ray ray1 = new Ray(transform.position + transform.TransformDirection(rayOffset1), transform.TransformDirection(Vector3.forward)); // Apply the offset and direction transformation
        Ray ray2 = new Ray(transform.position + transform.TransformDirection(rayOffset2), transform.TransformDirection(Vector3.forward)); // Apply the offset and direction transformation
        Ray ray3 = new Ray(transform.position + transform.TransformDirection(rayOffset3), transform.TransformDirection(Vector3.forward)); // Apply the offset and direction transformation
        if (CheckHit(ray1, ray2, ray3) && !isAttack1 && !isAttack2)
        {
            canMove = false;
            lastAnimTime = Time.time;
            isAttack1 = true;
            anim.SetBool("attack1", true);
        }

        Ray ray4 = new Ray(transform.position + transform.TransformDirection(rayOffset4), transform.TransformDirection(Vector3.forward)); // Apply the offset and direction transformation
        if (CheckHit1(ray4) && !isAttack1 && !isAttack2 && Time.time - lastAnimTime > Attack2CooldownTime)
        {
            canMove = false;
            lastAnimTime = Time.time;
            isAttack2 = true;
            anim.SetBool("attack2", true);
            //StartCoroutine(MoveForwardDuringAttack2());
            StartCoroutine(DelayedMoveForward(Attack2_moveDuration)); // Delay of 0.5 seconds
        }


        //if (!canMove && Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    canMove = false;
        //    lastAnimTime = Time.time;
        //    isAttack2 = true;
        //    anim.SetBool("chase", false);
        //    anim.SetBool("attack2", true);
        //}

        if (health <= 0)
        {
            canMove = false;
            isDie = true;
            anim.SetBool("die", true);
        }
    }

    public void getDamage(float damageAmount)
    {
        health -= damageAmount;

        var h_bar = gameObject.GetComponentInChildren<HealthBar>();
        if (h_bar != null)
        {
            h_bar.takeDamage(damageAmount);
        }

        var hb = HB.GetComponent<HealthBar>();
        if (hb != null)
        {
            hb.takeDamage(damageAmount);
        }
    }

    public void SeePlayer()
    {
        canMove = false;
        lastAnimTime = Time.time;
        isSeePlayer = true;
        anim.SetBool("seePlayer", true);

        SoundRoar.pitch = SoundRoarPitch;
        Invoke("Sound_Roar", SoundRoarDelay);
    }

    public void set_CanMove()
    {
        canMove = true;
    }

    private bool CheckHit(Ray ray1, Ray ray2, Ray ray3)
    {
        if (health > 0)
        {
            bool R1 = Physics.Raycast(ray1, out RaycastHit hit1, rayLength, layerMask);
            bool R2 = Physics.Raycast(ray2, out RaycastHit hit2, rayLength, layerMask);
            bool R3 = Physics.Raycast(ray3, out RaycastHit hit3, rayLength, layerMask);
            if (R1 || R2 || R3)
            {
                //Debug.Log("Raycast ---> " + R1 + " - " + R2 + " - " + R3);
                return true;
            }
        }
        return false;
    }

    private bool CheckHit1(Ray ray1)
    {
        if (health > 0)
        {
            bool R1 = Physics.Raycast(ray1, out RaycastHit hit1, rayLength4, layerMask);
            
            if (R1)
            {
                //Debug.Log("Raycast ---> " + R1 + " - " + R2 + " - " + R3);
                return true;
            }
        }
        return false;
    }

    public void StartDealingDamage()
    {
        Invoke("Sound_Attack1", SoundAttack1Delay);

        Ray ray1 = new Ray(transform.position + transform.TransformDirection(rayOffset1), transform.TransformDirection(Vector3.forward)); // Apply the offset and direction transformation
        Ray ray2 = new Ray(transform.position + transform.TransformDirection(rayOffset2), transform.TransformDirection(Vector3.forward)); // Apply the offset and direction transformation
        Ray ray3 = new Ray(transform.position + transform.TransformDirection(rayOffset3), transform.TransformDirection(Vector3.forward)); // Apply the offset and direction transformation
        if (CheckHit(ray1, ray2, ray3))
        {
            if (playerGameObject != null)
            {
                var fighter = playerGameObject.GetComponent<Fighter>();

                if (fighter != null)
                {
                    fighter.getDamage(Attack1Damage);
                }
            }
        }
        else
        {
            Debug.Log("playe doged!");
        }
    }

    public void A2DealingDamage()
    {
        Invoke("Sound_Attack2", SoundAttack2Delay);

        Ray ray = new Ray(transform.position + transform.TransformDirection(rayOffset4), transform.TransformDirection(Vector3.forward)); // Apply the offset and direction transformation
        if (CheckHit1(ray))
        {
            if (playerGameObject != null)
            {
                var fighter = playerGameObject.GetComponent<Fighter>();

                if (fighter != null)
                {
                    fighter.getDamage(Attack2Damage);
                }
            }

        }
        else
        {
            Debug.Log("playe doged!");
        }
    }

    IEnumerator DelayedMoveForward(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        // Apply the forward movement after the delay
        transform.position += transform.forward * Attack2_moveDistance; // Adjust this value to control the distance
    }

    //IEnumerator MoveForwardDuringAttack2()
    //{
    //    float moveDuration = Attack2_moveDuration; // Duration of the forward movement (in seconds)
    //    float moveDistance = Attack2_moveDistance; // Distance to move forward
    //    float elapsedTime = Attack2_elapsedTime;

    //    Vector3 startingPosition = transform.position;
    //    Vector3 targetPosition = transform.position + transform.forward * moveDistance;

    //    while (elapsedTime < moveDuration)
    //    {
    //        transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / moveDuration);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    transform.position = targetPosition; // Ensure the final position is set
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Draw the first ray, applying the offset and transforming the direction to local space
        Gizmos.DrawRay(transform.position + transform.TransformDirection(rayOffset1), transform.TransformDirection(Vector3.forward) * rayLength);

        // Draw the second ray
        Gizmos.DrawRay(transform.position + transform.TransformDirection(rayOffset2), transform.TransformDirection(Vector3.forward) * rayLength);

        // Draw the third ray
        Gizmos.DrawRay(transform.position + transform.TransformDirection(rayOffset3), transform.TransformDirection(Vector3.forward) * rayLength);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.TransformDirection(rayOffset4), transform.TransformDirection(Vector3.forward) * rayLength4);

    }

    void Sound_Roar()
    {
        if (SoundRoar != null)
        {
            SoundRoar.Play();
        }
    }

    void Sound_Attack1()
    {
        if (SoundAttack1 != null)
        {
            SoundAttack1.PlayDelayed(SoundAttack1Delay);
        }
    }

    void Sound_Attack2()
    {
        if (SoundAttack2 != null)
        {
            SoundAttack2.PlayDelayed(SoundAttack2Delay);
        }
    }

    public float getHealth()
    {
        return health;
    }

}
