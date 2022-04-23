using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject shot;
    [SerializeField] private float currentReloadTime;
    [SerializeField] private float maxReloadTime;
    private bool laser;
    private Vector2 impactPoint;
    private RaycastHit2D laserRay;
    private GameObject myTank;
    private TankManager myManager;
    private Vector3[] Arraywithpositions = new Vector3[2];
    private LineRenderer lineRenderer;

    private bool shoot;

    private void Start()
    {
        shoot = false;
        currentReloadTime = 0;
        myTank = gameObject;
        myManager = myTank.GetComponent<TankManager>();
        lineRenderer = myTank.GetComponent<LineRenderer>();
        maxReloadTime = myManager.myReloadSpeed;

    }

    void Update()
    {
        maxReloadTime = myManager.myReloadSpeed;
        laser = myManager.laser;

        CheckLaser();

        if (currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
        }
        else
        {
            
            if (shoot)
            {
                GameObject bullet = Instantiate(shot, firepoint.position, firepoint.rotation);
                // Sending messsage containing name of tank firing the shot to the shot object so it knows where it came from
                bullet.SendMessage("Retriever", myTank);

                currentReloadTime = maxReloadTime;
            }   
        }
    }

    private void CheckLaser()
    {
        if (laser)
        {
            laserRay = Physics2D.Raycast(firepoint.position, myTank.transform.up);
            impactPoint = laserRay.point;
            Arraywithpositions[0] = firepoint.position;
            Arraywithpositions[1] = impactPoint;
            lineRenderer.SetPositions(Arraywithpositions);
        }
        else
        {
            Arraywithpositions[0] = firepoint.position;
            Arraywithpositions[1] = firepoint.position;
            lineRenderer.SetPositions(Arraywithpositions);
        }
    }
    public void Fire(InputAction.CallbackContext input)
    {
        if (input.performed) shoot = true;
        if (input.canceled) shoot = false;
    }

}
