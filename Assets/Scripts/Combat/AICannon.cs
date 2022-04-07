using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICannon : MonoBehaviour
{

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject shot;
    [SerializeField] private float currentReloadTime;
    [SerializeField] private float maxReloadTime;
    private GameObject myTank;

    private RaycastHit2D visionRay;
    private Vector2 vectorToTarget = new Vector2();

    private void Start()
    {
        currentReloadTime = 0;
        maxReloadTime = 1;
        myTank = gameObject;
    }

    void Update()
    {
        vectorToTarget = myTank.GetComponent<TankController>().myTransform.up;
        visionRay = Physics2D.Raycast(firepoint.position, vectorToTarget);
        Debug.DrawRay(firepoint.position, vectorToTarget, Color.green);

        if (currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
        }
        else
        {
            if (playerInSight())
            {
                Shoot();
                currentReloadTime = maxReloadTime;
            }
        }
    }

    // Checks if the player is in vision to be shot at
    private bool playerInSight()
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
        // Sending the name of the tank firing the shot to the shot object so it knows where it came from
        bullet.SendMessage("Retriever", myTank);
    }

}
