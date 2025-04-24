using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;
    [SerializeField] float jumpSpeed = 0;

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    [Header("Hit 3 Settings")]
    public Vector3 direction_hit3 = Vector3.forward; // Direction to move
    public float distance_hi3 = 5f; // Distance to move
    public float duration_hit3 = 2f; // Duration to move the distance
    private float moveSpeed_hit3 = 0;
    private float elapsedTime_hit3 = 0f;

    public bool isGrounded;
    bool isJumping;
    float ySpeed;
    bool is_Shift = false;

    public bool canMove = true;

    Quaternion targrtRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;
    Fighter fighter;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        fighter = GetComponent<Fighter>();

        // Calculate velocity for hit3
        moveSpeed_hit3 = distance_hi3 / duration_hit3;
    }


    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        float moveAmount = 0;

        is_Shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        moveAmount = Mathf.Clamp(Mathf.Abs(h) + Mathf.Abs(v), 0f, 0.5f);

        if ( is_Shift && moveAmount > 0 )
        {
            moveAmount = 1;
        }



        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.PlanerRotation * moveInput;

        GroundCheck();

        animator.SetBool("isJumping", false);

        if (isGrounded)
        {

            //Debug.Log("On Ground");

            ySpeed = -0.5f;
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);

            // Jump
            //Checking is the jump button is pressed
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("isJumping", true);
                isJumping = true;
                ySpeed = jumpSpeed;

            }
        }
        else
        {
            //Debug.Log("Not On Ground");
            animator.SetBool("isGrounded", false);

            //Debug.Log("Y Spped = " + ySpeed);

            if((isJumping && ySpeed < -3))
            {
                animator.SetBool("isFalling", true);
            }

            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
        
        

        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;

        if(canMove)
        {
            characterController.Move(velocity * Time.deltaTime);   
        }

        if (moveAmount > 0)
        {
            targrtRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targrtRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);

        if (fighter.isHit3)
        {

            if(moveAmount ==  0)
            {
                moveInput = (new Vector3(0, 0, 1)).normalized;
                moveDir = transform.forward * moveInput.magnitude;
            }

            //Debug.Log("On hit_3");
            velocity = moveDir * moveSpeed_hit3;
            if (elapsedTime_hit3 < duration_hit3)
            {
                characterController.Move(velocity * Time.deltaTime);
            }
        }
        else
        {
            //Debug.Log("Not on hit_3");
        }

    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    public bool get_is_Shift => is_Shift;
    public bool get_CanMove => canMove;

}
