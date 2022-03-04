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

    private bool shoot;

    private void Start()
    {
        shoot = false;
        currentReloadTime = 0;
        maxReloadTime = 1;
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
                Instantiate(shot, firepoint.position, firepoint.rotation);
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
