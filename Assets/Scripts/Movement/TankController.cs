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
        UpdateVelocity();
        UpdateRotation();
    }

    private void UpdateVelocity()
    {
        vectorMag = vectorToTarget.magnitude;

        /* The tank drives close enough to fire point blank.
         * In practice this would most likely actually be determined by
         * whether or not the tank has a shot at the target. */
        if (vectorMag <= 2f)
        {
            rigidBody.velocity = 0 * myForward.up;
        }else
        {
            rigidBody.velocity = mySpeed * myForward.up;
        }

    }
    private void UpdateRotation()
    {
        angleToTarget = Vector2.SignedAngle(myForward.up, vectorToTarget);

        // Deciding whether the tank needs to turn or not based on angle
        if (angleToTarget > 2f)
        {
            currentRotationSpeed = 1;

        }else if(angleToTarget < -2f)
        {
            currentRotationSpeed = -1;

        }else
        {
            currentRotationSpeed = 0;
        }

        rigidBody.angularVelocity =  currentRotationSpeed * maxRotationSpeed;
    }
    private void UpdateVector()
    {
        vectorToTarget.x = myTarget.position.x - myTransform.position.x;
        vectorToTarget.y = myTarget.position.y - myTransform.position.y;
    }

}