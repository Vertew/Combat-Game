using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CommandProcessor))]
public class PlayerController : MonoBehaviour, IEntity
{

    [SerializeField] private Transform myForward;
    //[SerializeField] private Transform myTarget;

    private Rigidbody2D rigidBody;
    private float mySpeed;
    private float maxRotationSpeed;
    private TankManager myManager;
    private float angleToTarget;
    private float vectorMag;
    private Vector2 vectorToTarget = new Vector2();
    private Transform myTransform;
    Rigidbody2D IEntity.rb { get { return rigidBody; } }
    private Vector2 playerInput;

    private CommandProcessor commandProcessor;
    private bool playerForward, playerBack, playerLeft, playerRight;

    private void Awake()
    {
        myManager = gameObject.GetComponent<TankManager>();
        rigidBody = GetComponent<Rigidbody2D>();
        myTransform = transform;
        commandProcessor = GetComponent<CommandProcessor>();
        mySpeed = myManager.mySpeed;
        maxRotationSpeed = myManager.myRotationSpeed;
    }

    void Update()
    {
        // These values needs to be continuously updated
        // in case they are altered by powerups.
        mySpeed = myManager.mySpeed;
        maxRotationSpeed = myManager.myRotationSpeed;

        rigidBody.angularVelocity = 0f;
        rigidBody.velocity = Vector2.zero;

        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        vectorMag = vectorToTarget.magnitude;

        if (playerForward)
        {
            commandProcessor.ExecuteCommand(new DriveCommand(this, Time.timeSinceLevelLoad, myForward.up, mySpeed));
        }
        if (playerBack)
        {
            commandProcessor.ExecuteCommand(new DriveCommand(this, Time.timeSinceLevelLoad, myForward.up, -mySpeed));
        }
        if (playerLeft)
        {
            commandProcessor.ExecuteCommand(new RotateCommand(this, Time.timeSinceLevelLoad, maxRotationSpeed));
        }
        if (playerRight)
        {
            commandProcessor.ExecuteCommand(new RotateCommand(this, Time.timeSinceLevelLoad, -maxRotationSpeed));
        }
    }

    public void Forward(InputAction.CallbackContext input)
    {
        if (input.started) {playerForward = true;}
        if (input.canceled){playerForward = false;}
    }

    public void Back(InputAction.CallbackContext input)
    {
        if (input.started) { playerBack = true; }
        if (input.canceled) { playerBack = false; }
    }

    public void Left(InputAction.CallbackContext input)
    {
        if (input.started) { playerLeft = true; }
        if (input.canceled) { playerLeft = false; }
    }

    public void Right(InputAction.CallbackContext input)
    {
        if (input.started) { playerRight = true; }
        if (input.canceled) { playerRight = false; }
    }


}