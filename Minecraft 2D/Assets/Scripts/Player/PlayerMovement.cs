using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private bool isJumping = false;
    private Rigidbody2D playerRigidbody;
    private Animator animator;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");

        float moveX = movementSpeed * Time.fixedDeltaTime * inputX;
        playerRigidbody.velocity = new Vector2(moveX, playerRigidbody.velocity.y);

        Animate(inputX);
        if (isJumping)
        {
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    private void Animate(float inputX)
    {
        animator.SetFloat("Speed", Mathf.Abs(inputX));
        if (inputX > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (inputX < -0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}