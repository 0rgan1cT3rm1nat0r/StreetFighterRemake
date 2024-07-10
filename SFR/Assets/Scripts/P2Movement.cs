using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

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
        if (isAttacking) return;  // Prevent movement while attacking

        if (Input.GetKey(KeyCode.LeftArrow) && isGrounded && !isCrouching)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            animator.SetBool("Walking", true);
            //print(Walking);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) && isGrounded && !isCrouching)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            animator.SetBool("Walking2", true);
        }
        else
        {
            animator.SetBool("Walking2", false);
        }

        // Handle diagonal jumps
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && isGrounded && !isCrouching)
        {
            DiagonalJump(Vector2.left);
            animator.SetBool("isDiagonalJumping", true);
        }
        else
        {
            animator.SetBool("isDiagonalJumping", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && isGrounded && !isCrouching)
        {
            DiagonalJump(Vector2.right);
            animator.SetBool("isDiagonalJumping2", true);
        }
        else
        {
            animator.SetBool("isDiagonalJumping2", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !isCrouching)
        {
            Jump();
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
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
        if (Input.GetKeyDown(KeyCode.K))
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

        UpdateAnimator();
    }

    void Move(float move)
    {
        Vector2 movement = new Vector2(move * speed, rb.velocity.y);
        rb.velocity = movement;
    }

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
        // Reset player's collider size and position if needed
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
}