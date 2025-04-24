using UnityEngine;
using UnityEngine.AI;

public class EnemySkeleton : MonoBehaviour
{
    [Header("Enemy Settings")]
    public NavMeshAgent enemy;
    public Transform target;
    public bool canMove = false;
    [SerializeField] private float health = 200f;
    [SerializeField] private float damageDeal = 10f;
    private GameObject playerGameObject;
    [SerializeField] GameObject healOrb;
    private bool hasInstantiated = false;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLength = 2f; // Adjustable length of the ray
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 rayOffset = new Vector3(0, 1.46f, 0); // Offset for ray

    [Header("Debug varables")]
    [SerializeField]  bool isWalking;
    [SerializeField]  bool isHit;
    [SerializeField]  bool isDead;

    [Header("Trigger")]
    public bool T_lock = false;

    //Sound
    [Header("Sound Settings")]
    [SerializeField] private AudioSource SoundHit;
    [SerializeField] private float SoundHitDelay = 0f;
    [SerializeField] private AudioSource SoundWalk;
    [SerializeField] private float SoundWalkPitch = 1f;
    [SerializeField] private AudioSource SoundSword;

    private Animator anim;

    private void Start()
    {
        playerGameObject = target.gameObject;
        anim = GetComponent<Animator>();
        canMove = false;
        anim.SetBool("canMove", false);

    }

    private void Update()
    {

        if (canMove && T_lock)
        {
            enemy.SetDestination(target.position);
            anim.SetBool("walking", true);
            isWalking = true;
            SoundWalk.enabled = true;
            SoundWalk.pitch = SoundWalkPitch;
            anim.SetBool("canMove", true);
        }
        else
        {
            anim.SetBool("walking", false);
            isWalking = false;
            SoundWalk.enabled = false;
            anim.SetBool("canMove", false);
        }

        if (health <= 0)
        {
            canMove = false;
            anim.SetBool("canMove", false);
            enemy.enabled = false; // Disable NavMeshAgent to stop movement
            anim.SetBool("isDie", true);
            anim.SetBool("walking", false);
            isDead = true;
            isWalking = false ;
            Destroy(gameObject, 3f); // Delayed destruction

            if (!hasInstantiated)
            {
                Instantiate(healOrb, transform.position, Quaternion.identity);
                hasInstantiated = true;
            }

            return;
        }

        Ray ray = new Ray(transform.position + rayOffset, transform.forward); // Apply the offset
        CheckHit(ray);
    }

    private void CheckHit(Ray ray)
    {
        if (health > 0)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
            {
                Debug.Log("Player Hit");
                canMove = false;
                anim.SetBool("isHit", true);
                anim.SetBool("walking", false);
                isHit = true;
                isWalking = false ;
                SoundSword.enabled = true;
            }
            else
            {

                canMove = true;
                anim.SetBool("isHit", false);
                anim.SetBool("walking", true);
                isHit = false;
                isWalking = true;
                SoundSword.enabled = false;

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + rayOffset, transform.forward * rayLength); // Apply the offset here
    }

    public void getDamage(float damageAmount)
    {
        health -= damageAmount;

        var h_bar = gameObject.GetComponentInChildren<HealthBar>();
        if (h_bar != null)
        {
            h_bar.takeDamage(damageAmount);
            Invoke("Sound_hit", SoundHitDelay);
        }
    }

    public void enemyDealDamage()
    {
        if (playerGameObject != null)
        {
            var fighter = playerGameObject.GetComponent<Fighter>();

            if (fighter != null)
            {
                fighter.getDamage(damageDeal);
            }
        }
    }

    public void set_T_lock()
    {
        T_lock = true;
    }

    public float getHealth()
    {
        return health;
    }

    void Sound_hit()
    {
        if (SoundHit != null)
        {
            SoundHit.Play();
        }
    }
}
