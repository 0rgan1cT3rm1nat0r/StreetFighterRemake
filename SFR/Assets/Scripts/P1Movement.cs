using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private bool isCrouching = false;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //float move = Input.GetAxis("Horizontal");

        if (isAttacking) return;  // Prevent movement while attacking

        if (Input.GetKey(KeyCode.A) && isGrounded)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && isGrounded)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        // Handle diagonal jumps
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && isGrounded)
        {
            DiagonalJump(Vector2.right);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && isGrounded)
        {
            DiagonalJump(Vector2.left);
        }

        /*
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && isGrounded)
        {
            DiagonalJump();
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && isGrounded)
        {
            DiagonalJump2();
        }
        */

        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isCrouching)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            Crouch();
        }
        else
        {
            StandUp();
        }

        // Handle attacks
        if (Input.GetKeyDown(KeyCode.J)) // Replace KeyCode.J with your preferred attack key
        {
            if (isCrouching)
            {
                CrouchAttack();
            }
            else
            {
                Attack();
            }
        }

        // Update animator
        UpdateAnimator();

        //Move(move);
        //UpdateAnimator(move);
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
        Debug.Log("jump");
    }

    void DiagonalJump(Vector2 direction)
    {
        isGrounded = false;
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
        Debug.Log("diagonal jump");
    }
    /*
    void DiagonalJump()
    {
        isGrounded = false;
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        Debug.Log("forwardjump");
    }

    void DiagonalJump2()
    {
        isGrounded = false;
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        Debug.Log("backjump");
    }
    */

    void Crouch()
    {
        isCrouching = true;
        // Adjust player's collider size and position if needed
        Debug.Log("crouch");
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
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isCrouching", isCrouching);
    }

    //void UpdateAnimator(float move)
    //{
    //animator.SetFloat("Speed", Mathf.Abs(move));
    //animator.SetBool("isGrounded", isGrounded);
    //animator.SetBool("isCrouching", isCrouching);
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}