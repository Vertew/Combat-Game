using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
public class TankController : MonoBehaviour
{
    // This class handles the automated movement for a computer opponent in singleplayer.
    // It uses the A* pathfinding project package in conjunction with my own 
    // movement functions.

    [SerializeField] private Transform myForward;
    private float mySpeed;
    private float maxRotationSpeed;
    [SerializeField] private Transform myTarget;
    [SerializeField] float nextWaypointDistance = 3f;

    private Rigidbody2D rigidBody;
    private float currentRotationSpeed;
    private float angleToTarget;
    private float vectorMag;
    private float vectorMag2;
    private TankManager myManager;
    private Vector2 vectorToTarget = new Vector2();
    private Vector2 vectorToTank = new Vector2();

    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;

    public Transform myTransform;

    private void Awake()
    {
        myManager = gameObject.GetComponent<TankManager>();
        rigidBody = GetComponent<Rigidbody2D>();
        myTransform = transform;
        mySpeed = myManager.mySpeed -0.5f;
        maxRotationSpeed = myManager.myRotationSpeed;
    }

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(myTransform.position, myTarget.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }

    private void FixedUpdate()
    {

        mySpeed = myManager.mySpeed - 0.5f;
        maxRotationSpeed = myManager.myRotationSpeed;

        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        UpdateVector();



        UpdateVelocity();

        float distance = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        UpdateRotation();
    }

    private void UpdateVelocity()
    {

        vectorMag = vectorToTank.magnitude;
        vectorMag2 = vectorToTarget.magnitude;

        if (vectorMag <= 2f)
        {
            rigidBody.velocity = 0 * myForward.up;
        }
        else
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
        vectorToTarget = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized;
        vectorToTank = myTarget.position - myTransform.position;
    }

}