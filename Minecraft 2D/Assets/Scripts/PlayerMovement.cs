using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private bool isJumping = false;
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.position += movementSpeed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), 0f);

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
}
