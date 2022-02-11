using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject shot;
    [SerializeField] private float currentReloadTime;
    [SerializeField] private float maxReloadTime;

    private void Start()
    {
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                currentReloadTime = maxReloadTime;
            }
        } 
    }

    private void Shoot()
    {
        Instantiate(shot, firepoint.position, firepoint.rotation);
    }

}
