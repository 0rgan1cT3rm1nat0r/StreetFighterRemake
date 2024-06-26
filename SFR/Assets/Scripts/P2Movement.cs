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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //float move = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftArrow) && isGrounded)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) && isGrounded)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            DiagonalJump();
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            DiagonalJump2();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !isCrouching)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
        {
            Crouch();
        }
        else
        {
            StandUp();
        }

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