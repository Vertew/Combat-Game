using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TankController : MonoBehaviour
{

    [SerializeField] private Transform myForward;
    [SerializeField] private float mySpeed;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
    }
    void UpdateVelocity()
    {
        rigidBody.velocity = mySpeed * myForward.up;
    }
}
