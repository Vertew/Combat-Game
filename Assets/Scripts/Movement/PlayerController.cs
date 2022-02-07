using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform myForward;
    [SerializeField] private float mySpeed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private Transform myTarget;

    private Rigidbody2D rigidBody;
    //private float currentRotationSpeed = 0f;
    private float angleToTarget;
    private float vectorMag;
    private Vector2 vectorToTarget = new Vector2();
    private Transform myTransform;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }

    private void FixedUpdate()
    {
        UpdateVector();
    }
    void Update()
    {

        rigidBody.angularVelocity = 0f;
        rigidBody.velocity = Vector2.zero;

        UpdateVelocity();
        UpdateRotation();
    }

    private void UpdateVelocity()
    {
        vectorMag = vectorToTarget.magnitude;

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.velocity = mySpeed * myForward.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.velocity = -mySpeed * myForward.up;
        }


    }
    private void UpdateRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.angularVelocity = maxRotationSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.angularVelocity = -maxRotationSpeed;
        }
    }
    private void UpdateVector()
    {
        vectorToTarget.x = myTarget.position.x - myTransform.position.x;
        vectorToTarget.y = myTarget.position.y - myTransform.position.y;
    }

}
