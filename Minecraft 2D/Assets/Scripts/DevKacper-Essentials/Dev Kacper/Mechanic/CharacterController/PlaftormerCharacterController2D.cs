using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlaftormerCharacterController2D : MonoBehaviour
{
    private enum GroundCheckType
    {
        Circle,
        Square
    }


    [Header("General")]
    [SerializeField] private float jumpForce = 150f;
    [SerializeField] private float movementSpeed = 50f;

    [Header("Is Grounded settings")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private GroundCheckType groundCheckType;
    [SerializeField] private string[] groundTags;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody2D rigidbody;

    private const float CircleGroundedRadius = 0.15f;
    private const float BoxGroundedHeight = 0.2f;
    private float BoxGroundedWidth = 1f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if(GetComponent<BoxCollider2D>() != null)
        {
            BoxGroundedWidth = GetComponent<BoxCollider2D>().size.x;
        }
    }

    private void FixedUpdate()
    {
        switch(groundCheckType)
        {
            case GroundCheckType.Circle:
                CircleGroundCheck();
                break;
            case GroundCheckType.Square:
                SquareGroundCheck();
                break;
        }
    }

    private void CircleGroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, CircleGroundedRadius);
        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.layer == groundLayer || IsGroundTag(collider.tag))
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void SquareGroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheckTransform.position, new Vector2(BoxGroundedWidth, BoxGroundedHeight), 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == groundLayer || IsGroundTag(collider.tag))
            {
                isGrounded = true;
                return;
            }
        }
    }

    private bool IsGroundTag(string colliderTag)
    {
        foreach(string tag in groundTags)
        {
            if(tag == colliderTag)
            {
                return true;
            }
        }
        return false;
    }

    public void Move(Vector3 move)
    {
        transform.position += move;
    }

    public void Jump(float jumpForce)
    {
        rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
