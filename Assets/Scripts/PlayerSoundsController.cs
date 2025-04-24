using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    [Header("Player Sound Settings")]
    [Space(10)]

    [Header("Walk & Run")]
    [SerializeField] private AudioSource SoundWalk;
    [SerializeField] private float SoundWalkDelay = 0f;
    [SerializeField] private float SoundWalkPitch = 1.2f;
    [SerializeField] private float SoundRunPitch = 1.4f;


    [Header("Dodge")]
    [SerializeField] private AudioSource SoundDodge;
    [SerializeField] private float SoundDodgeDelay = 0.4f;

    [Header("Sound Single Atack")]
    [SerializeField] private AudioSource SoundSingleAttack;
    [SerializeField] private float SoundSingleAttackDelay = 0.4f;

    [Header("Sound Combo Atack Hit 1")]
    [SerializeField] private AudioSource SoundComboHit1;
    [SerializeField] private float SoundComboHit1Delay = 0f;

    [Header("Sound Combo Atack Hit 2")]
    [SerializeField] private AudioSource SoundComboHit2;
    [SerializeField] private float SoundComboHit2Delay = 0f;

    [Header("Sound Combo Atack Hit 3")]
    [SerializeField] private AudioSource SoundComboHit3;
    [SerializeField] private float SoundComboHit3Delay = 0f;

    PlayerController playerController;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)) && playerController.get_CanMove)
        {
            SoundWalk.enabled = true;

            // Run Sound & Walk Sound
            if (Input.GetKey(KeyCode.LeftShift))
            {
                SoundWalk.pitch = SoundRunPitch;
            }
            else
            {
                SoundWalk.pitch = SoundWalkPitch;
            }

        }
        else
        {

            SoundWalk.enabled = false;
        }

    }

    // Single Attack Sound
    public void Sound_SingleAttack_WithDelay()
    {
        if (SoundSingleAttack != null)
        {
            Invoke("Sound_SingleAttack", SoundSingleAttackDelay);
        }
    }

    void Sound_SingleAttack()
    {
        if (SoundSingleAttack != null)
        {
            SoundSingleAttack.Play();
        }

    }

    // Combo hit 1
    public void Sound_ComboHit1_WithDelay()
    {
        if (SoundComboHit1 != null)
        {
            Invoke("Sound_ComboHit1", SoundComboHit1Delay);
        }
    }

    void Sound_ComboHit1()
    {
        if (SoundComboHit1 != null)
        {
            SoundComboHit1.Play();
        }

    }

    // Combo hit 2
    public void Sound_ComboHit2_WithDelay()
    {
        if (SoundComboHit2 != null)
        {
            Invoke("Sound_ComboHit2", SoundComboHit2Delay);
        }
    }

    void Sound_ComboHit2()
    {
        if (SoundComboHit2 != null)
        {
            SoundComboHit2.Play();
        }

    }

    // Combo hit 3
    public void Sound_ComboHit3_WithDelay()
    {
        if (SoundComboHit3 != null)
        {
            Invoke("Sound_ComboHit3", SoundComboHit3Delay);
        }
    }

    void Sound_ComboHit3()
    {
        if (SoundComboHit3 != null)
        {
            SoundComboHit3.Play();
        }

    }

    // Dodge Sound
    public void Sound_Dodge_WithDelay()
    {
        if (SoundDodge != null)
        {
            Invoke("Sound_Dodge", SoundDodgeDelay);
        }
    }

    void Sound_Dodge()
    {
        if (SoundDodge != null)
        {
            SoundDodge.Play();
        }

    }

    // Walk Sound
    public void Sound_Walk_WithDelay()
    {
        if (SoundWalk != null)
        {
            Invoke("Sound_Walk", SoundWalkDelay);
        }
    }

    void Sound_Walk()
    {
        if (SoundWalk != null)
        {
            SoundWalk.Play();
        }

    }
}
