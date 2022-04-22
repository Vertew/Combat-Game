using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class TankManager : MonoBehaviour
{

    public int myScore;
    public int myHealth;
    public float mySpeed;
    public float myRotationSpeed;
    public string tankName;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Quaternion startRotation;
    private GameObject myTank;
    private string objName;
    private bool powerUp;
    [SerializeField] private float powerTimerMax;
    [SerializeField] private float currentPowerTimer;



    void Start()
    {
        GameEvents.current.OnTankHit += OnHit;
    }

    private void Awake()
    {
        objName = gameObject.name;
        myTank = gameObject;
        spawnPoint = myTank.transform.position;
        startRotation = myTank.transform.rotation;
        powerTimerMax = 10f;
        powerUp = false;
        SetValues();
        GetScore();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // If a projectile enters the tank it loses health,
        // if health goes to 0, the tank is destroyed.
        if (hitInfo.CompareTag("Projectile"))
        {
            myHealth--;
            if (myHealth == 0)
            {
                GameEvents.current.TankKilledTrigger();
            }
        }
        else if (hitInfo.CompareTag("PowerUp"))
        {
            // The tank can only have one powerup at a time.
            // Each powerup lasts 10 seconds and then goes away.
            if (!powerUp)
            {
                mySpeed += 1;
                myRotationSpeed += 100;
                hitInfo.gameObject.SetActive(false);
                powerUp = true;
            } 
        }
    }

    // When a shot hits the tank, the round restarts, i.e. tank restarts at spawn position
    private void OnHit()
    {
        MainManager.Instance.updateScore(myScore, objName);
        myTank.transform.SetPositionAndRotation(spawnPoint, startRotation);
    }

    private void GetScore()
    {
        if (objName == "TankPlayer")
        {
            myScore = MainManager.Instance.score1;
        }
        else
        {
            myScore = MainManager.Instance.score2;
        }
    }

    // Sets all tank values to their defaults
    private void SetValues()
    {
        myHealth = 3;
        mySpeed = 1;
        myRotationSpeed = 100;
        currentPowerTimer = powerTimerMax;
    }

    void Update()
    {
        // If the tank has a powerup, the cooldown takes place.
        if (powerUp)
        {
            if (currentPowerTimer > 0)
            {
                currentPowerTimer -= Time.deltaTime;
            }
            else
            {
                powerUp = false;
                SetValues();
            }
        }
    }


    private void OnDestroy()
    {
        MainManager.Instance.updateScore(myScore, objName);
        GameEvents.current.OnTankHit -= OnHit;
    }

}
