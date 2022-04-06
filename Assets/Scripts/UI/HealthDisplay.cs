using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    [SerializeField] private Text healthText;
    [SerializeField] private GameObject tankObj;
    private TankManager tankManager;
    void Start()
    {
        tankManager = tankObj.GetComponent<TankManager>();
    }

    private void Update()
    {
        healthText.text = tankManager.tankName + " " + tankManager.myHealth.ToString();
    }

}
