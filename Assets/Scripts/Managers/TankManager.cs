using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class TankManager : MonoBehaviour
{

    [SerializeField] public int myScore;
    [SerializeField] public int myHealth;
    [SerializeField] public string tankName;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Quaternion startRotation;
    private GameObject myTank;
    private string objName;



    void Start()
    {
        GameEvents.current.OnTankHit += OnHit;
        //GetScore();
    }

    private void Awake()
    {
        objName = gameObject.name;
        myHealth = 3;
        myTank = gameObject;
        spawnPoint = myTank.transform.position;
        startRotation = myTank.transform.rotation;
        GetScore();
        Debug.Log("Score: " + myScore);
    }

    // If a projectile enters the tank it loses health
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        myHealth--;
        if (myHealth == 0) 
        {
            GameEvents.current.TankKilledTrigger();
        }
    }

    // When a shot hits a tank, the round restarts, i.e. tanks restart at spawn positions
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

    void Update()
    {

    }


    private void OnDestroy()
    {
        MainManager.Instance.updateScore(myScore, objName);
        GameEvents.current.OnTankHit -= OnHit;
    }

}
