using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TankController : MonoBehaviour
{

    [SerializeField] private Transform myForward;
    [SerializeField] private float mySpeed;
    [SerializeField] private float myRotationSpeed;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
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
        rigidBody.angularVelocity = myRotationSpeed;
    }


}
