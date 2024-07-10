using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public string attackTargetTag = "Enemy"; // The tag of the objects you want to detect during an attack

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private bool isCrouching = false;
    private bool isAttacking = false;
    //private bool Walking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        //if (isAttacking) return;  // Prevent movement while attacking

        if (Input.GetKey(KeyCode.A) && isGrounded && !isCrouching)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            animator.SetBool("Walking2", true);
            //print(Walking);
        }
        else
        {
            animator.SetBool("Walking2", false);
        }

        if (Input.GetKey(KeyCode.D) && isGrounded && !isCrouching)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        // Handle diagonal jumps
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && isGrounded && !isCrouching)
        {
            DiagonalJump(Vector2.right);
            animator.SetBool("isDiagonalJumping", true);
        }
        else
        {
            animator.SetBool("isDiagonalJumping", false);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && isGrounded && !isCrouching)
        {
            DiagonalJump(Vector2.left);
            animator.SetBool("isDiagonalJumping2", true);
        }
        else
        {
            animator.SetBool("isDiagonalJumping2", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isCrouching)
        {
            Jump();
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            Crouch();
            animator.SetBool("isCrouching", true);
        }
        else
        {
            StandUp();
            animator.SetBool("isCrouching", false);
        }

        // Handle attacks
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isCrouching)
            {
                CrouchAttack();
            }
            if (!isCrouching)
            {
                Attack();
            }
        }
        else
        {
            EndAttack();
        }
        UpdateAnimator();

    }

    /*void Move(float move)
    {
        Vector2 movement = new Vector2(move * speed, rb.velocity.y);
        rb.velocity = movement;
    }*/

    void Jump()
    {
        isGrounded = false;
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        //Debug.Log("jump");
    }

    void DiagonalJump(Vector2 direction)
    {
        isGrounded = false;
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
        //Debug.Log("diagonal jump");
    }

    void Crouch()
    {
        isCrouching = true;
        // Adjust player's collider size and position if needed
        //Debug.Log("crouch");
    }

    void StandUp()
    {
        isCrouching = false;
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        Debug.Log("attack");
    }

    void CrouchAttack()
    {
        isAttacking = true;
        animator.SetTrigger("CrouchAttack");
        Debug.Log("crouch attack");
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    void UpdateAnimator()
    {
        float move = Input.GetAxis("Horizontal");
        //animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isCrouching", isCrouching);
        //rOB IS MISSING A FEW EXtTRA CHROMOSOMES  
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Debug.Log("Hit " + other.name);
            // Handle the collision with the attack target here
        }
    }
}