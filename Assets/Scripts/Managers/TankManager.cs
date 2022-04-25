using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TankManager : MonoBehaviour
{
    // The TankManager class handles general aspects of each tank such as
    // storing general values and handling being hit, being killed, and
    // picking up powerups.

    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private float currentPowerTimer;

    private GameObject myTank;
    private PowerUp powerUpValues;
    public int myScore, myHealth;
    public float mySpeed, myRotationSpeed, myReloadSpeed, myShotSpeed, powerTimerMax;
    public bool laser, invun, powerUp, computer;
    public string tankName, myPowerup, objName;

    private void Awake()
    {
        objName = gameObject.name;
        myTank = gameObject;
        spawnPoint = myTank.transform.position;
        startRotation = myTank.transform.rotation;
        powerTimerMax = 10f;
        myHealth = 3;
        SetValueDefaults();
        CheckComputer();
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
            if (!invun)
            {
                myHealth--;
            }
            if (myHealth == 0)
            {
                MainManager.Instance.levelLoser = gameObject.name;
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
                AdjustValues(powerUpValues.speed, powerUpValues.rotationSpeed, powerUpValues.reloadSpeed, powerUpValues.shotSpeed, powerUpValues.laser, powerUpValues.invincible);
                hitInfo.gameObject.SetActive(false);
            } 
        }
    }

    // When a shot hits the tank, the round restarts, i.e. tank restarts at spawn position
    private void OnHit()
    {
        myTank.transform.SetPositionAndRotation(spawnPoint, startRotation);
        SetValueDefaults();
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
        mySpeed = 1;
        myRotationSpeed = 100;
        myReloadSpeed = 1f;
        myShotSpeed = 5;
        laser = false;
        invun = false;
        myPowerup = "No Powerup";
        currentPowerTimer = powerTimerMax;
        powerUp = false;
    }

    // Used to set new values when a powerup is picked up
    public void AdjustValues(int newSpeed, int newRotationSpeed, float newReloadSpeed, float newShotSpeed, bool newLaser, bool newInvun)
    {
        mySpeed = newSpeed;
        myRotationSpeed = newRotationSpeed;
        myReloadSpeed = newReloadSpeed;
        myShotSpeed = newShotSpeed;
        laser = newLaser;
        invun = newInvun;
        powerUp = true;
    }

    void Update()
    {

        MainManager.Instance.UpdateScore(myScore, objName);

        if (!computer)
        {
            AchievementManager.achievementCount[1] = myScore;
        }

        // If the tank has a powerup, the cooldown takes place.
        if (powerUp)
        {
            if (currentPowerTimer > 0)
            {
                currentPowerTimer -= Time.deltaTime;
            }
            else
            {
                SetValueDefaults();
            }
        }
    }

    private void CheckComputer()
    {
        if (gameObject.name == "TankEnemy")
        {
            computer = true;
        }
        else
        {
            computer = false;
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.OnTankHit -= OnHit;
    }

}
