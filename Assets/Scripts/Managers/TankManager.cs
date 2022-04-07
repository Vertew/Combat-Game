using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TankManager : MonoBehaviour
{

    [SerializeField] public int myScore;
    [SerializeField] public int myHealth;
    [SerializeField] public string tankName;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Quaternion startRotation;
    private GameObject myTank;


    void Start()
    {
        GameEvents.current.onTankHit += OnRoundRestart;
    }

    private void Awake()
    {
        myScore = 0;
        myHealth = 3;
        myTank = gameObject;
        spawnPoint = myTank.transform.position;
        startRotation = myTank.transform.rotation;
    }

    // If a projectile enters the tank it loses health
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        myHealth--;
    }

    // When a shot hits a tank, the round restarts, i.e. tanks restart at spawn positions
    private void OnRoundRestart()
    {
        myTank.transform.SetPositionAndRotation(spawnPoint, startRotation);
    }

    void Update()
    {

    }
}
