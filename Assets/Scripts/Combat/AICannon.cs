using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICannon : MonoBehaviour
{

    // This class handles the automated shooting for a computer component.
    // It essentially just fires a shot if the tank is aiming directly at
    // it's opponent. Uses the same shooting mechanics as the player.

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject shot;
    [SerializeField] private float currentReloadTime;
    [SerializeField] private float maxReloadTime;
    private GameObject myTank;
    private TankManager myManager;
    private TankController myController;

    private RaycastHit2D visionRay;
    private Vector2 vectorToTarget = new Vector2();

    private void Start()
    {
        currentReloadTime = 0;
        maxReloadTime = 1;
        myTank = gameObject;
        myManager = myTank.GetComponent<TankManager>();
        myController = myTank.GetComponent<TankController>();
    }

    void Update()
    {
        maxReloadTime = myManager.myReloadSpeed;
        vectorToTarget = myController.myTransform.up;
        visionRay = Physics2D.Raycast(firepoint.position, vectorToTarget);

        if (currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
        }
        else
        {
            if (PlayerInSight())
            {
                Shoot();
                currentReloadTime = maxReloadTime;
            }
        }
    }

    // Checks if the player is in position to be shot at
    private bool PlayerInSight()
    {
        {
            if (visionRay.collider != null)
            {
                if (visionRay.collider.gameObject.CompareTag("Tank"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(shot, firepoint.position, firepoint.rotation);
        bullet.SendMessage("Retriever", myTank);
    }
}
