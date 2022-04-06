using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TankManager : MonoBehaviour
{

    [SerializeField] private int myScore;
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

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(gameObject.name + " has been hit!");
        myHealth--;
        Debug.Log(gameObject.name + " HP = " + myHealth);
    }

    private void OnRoundRestart()
    {
        myTank.transform.SetPositionAndRotation(spawnPoint, startRotation);
    }

    void Update()
    {

    }
}
