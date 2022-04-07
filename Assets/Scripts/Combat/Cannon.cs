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
    private GameObject myTank;

    private bool shoot;

    private void Start()
    {
        shoot = false;
        currentReloadTime = 0;
        maxReloadTime = 1;
        myTank = gameObject;
    }

    void Update()
    {
        if (currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
        }
        else
        {
            
            if (shoot)
            {
                GameObject bullet = Instantiate(shot, firepoint.position, firepoint.rotation);
                // Sending tank firing the shot to the shot object so it knows where it came from
                bullet.SendMessage("Retriever", myTank);

                currentReloadTime = maxReloadTime;
                shoot = false;
            }
            
        }
    }

    public void Fire(InputAction.CallbackContext input)
    {
        if (input.performed) shoot = true;
    }

}
