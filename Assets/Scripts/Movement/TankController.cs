using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TankController : MonoBehaviour
{

    [SerializeField] private Transform myForward;
    [SerializeField] private float mySpeed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private Transform myTarget;

    private Rigidbody2D rigidBody;
    private float currentRotationSpeed = 0f;
    private float angleToTarget;
    private Vector2 vectorToTarget = new Vector2();
    private Transform myTransform;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
        UpdateRotation();
    }
    void UpdateVelocity()
    {
        rigidBody.velocity = mySpeed * myForward.up;
    }
    private void UpdateRotation()
    {
        vectorToTarget.x = myTarget.position.x - transform.position.x;
        vectorToTarget.y = myTarget.position.y - transform.position.y;

        angleToTarget = Vector2.SignedAngle(myForward.up, vectorToTarget);

        currentRotationSpeed = angleToTarget / 180f;

        rigidBody.angularVelocity =  currentRotationSpeed * maxRotationSpeed;
    }
}
