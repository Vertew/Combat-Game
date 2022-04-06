using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICannon : MonoBehaviour
{

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject myTank;
    [SerializeField] private GameObject shot;
    [SerializeField] private float currentReloadTime;
    [SerializeField] private float maxReloadTime;

    private RaycastHit2D visionRay;
    private Vector2 vectorToTarget = new Vector2();

    private void Start()
    {
        currentReloadTime = 0;
        maxReloadTime = 1;
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
        Instantiate(shot, firepoint.position, firepoint.rotation);
    }

}
