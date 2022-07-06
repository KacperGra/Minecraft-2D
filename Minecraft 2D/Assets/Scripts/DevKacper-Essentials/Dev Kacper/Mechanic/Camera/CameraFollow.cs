using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;

    private float cameraZValue = 10f;

    private void Awake()
    {
        cameraZValue = transform.position.z;
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            float moveValue = Time.fixedDeltaTime * moveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveValue);
            transform.position = new Vector3(transform.position.x, transform.position.y, cameraZValue);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
