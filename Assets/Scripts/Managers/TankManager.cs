using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class TankManager : MonoBehaviour
{

    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private float currentPowerTimer;

    private GameObject myTank;
    private PowerUp powerUpValues;
    public int myScore, myHealth;
    public float mySpeed, myRotationSpeed, myReloadSpeed, myShotSpeed, powerTimerMax;
    public bool laser, powerUp;
    public string tankName, myPowerup, objName;

    private void Awake()
    {
        objName = gameObject.name;
        myTank = gameObject;
        spawnPoint = myTank.transform.position;
        startRotation = myTank.transform.rotation;
        powerTimerMax = 10f;
        powerUp = false;
        SetValueDefaults();
    }

    void Start()
    {
        GameEvents.current.OnTankHit += OnHit;
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
                powerUpValues = hitInfo.GetComponent<PowerUp>();
                myPowerup = hitInfo.gameObject.name;
                AdjustValues(powerUpValues.speed, powerUpValues.rotationSpeed, powerUpValues.reloadSpeed, powerUpValues.shotSpeed, powerUpValues.laser);
                hitInfo.gameObject.SetActive(false);
                powerUp = true;
            } 
        }
    }

    // When a shot hits the tank, the round restarts, i.e. tank restarts at spawn position
    private void OnHit()
    {
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
    private void SetValueDefaults()
    {
        myHealth = 3;
        mySpeed = 1;
        myRotationSpeed = 100;
        myReloadSpeed = 1f;
        myShotSpeed = 5;
        laser = false;
        myPowerup = "No Powerup";
        currentPowerTimer = powerTimerMax;
    }

    // Used to set new values when a powerup is picked up
    public void AdjustValues(int newSpeed, int newRotationSpeed, float newReloadSpeed, float newShotSpeed, bool newLaser)
    {
        mySpeed = newSpeed;
        myRotationSpeed = newRotationSpeed;
        myReloadSpeed = newReloadSpeed;
        myShotSpeed = newShotSpeed;
        laser = newLaser;
    }

    void Update()
    {

        MainManager.Instance.updateScore(myScore, objName);

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
                SetValueDefaults();
            }
        }
    }


    private void OnDestroy()
    {
        GameEvents.current.OnTankHit -= OnHit;
    }

}
