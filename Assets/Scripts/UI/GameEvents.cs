using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;
    public event Action OnTankHit, OnTankKilled;

    public void Awake()
    {

        if (current != null)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
        DontDestroyOnLoad(gameObject);

    }

    public void TankHitTrigger()
    {
        if (OnTankHit != null)
        {
            OnTankHit();
        }
    }

    public void TankKilledTrigger()
    {
        if (OnTankKilled != null)
        {
            OnTankKilled();
        }
    }


}
