using UnityEngine;
//using UnityEngine.AI;

public class EnemySkeletonwelcome : MonoBehaviour
{
    [Header("Enemy Settings")]
    //public NavMeshAgent enemy;
    //public Transform target;
    private bool canMove = false;
    [SerializeField] private float health = 200f;

    // Hit Sound
    [SerializeField] private AudioSource SoundHit;
    [SerializeField] private float SoundHitDelay = 0f;




    private Animator anim;

    private void Start()
    {
        //playerGameObject = target.gameObject;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (health <= 0)
        {
            
            Destroy(gameObject, 3f); // Delayed destruction


            return;
        }

       
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

    void Sound_hit()
    {
        if(SoundHit != null)
        {
            SoundHit.Play();
        }
    }

    
}
