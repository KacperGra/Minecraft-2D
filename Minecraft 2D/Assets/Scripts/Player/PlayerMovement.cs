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
        float inputX = Input.GetAxis("Horizontal");

        transform.position += movementSpeed * Time.deltaTime * new Vector3(inputX, 0f);
        Animate(inputX);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if(isJumping)
        {
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    private void Animate(float inputX)
    {
        animator.SetFloat("Speed", Mathf.Abs(inputX));
        if(inputX > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(inputX < -0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
