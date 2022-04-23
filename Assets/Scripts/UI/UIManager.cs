using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text UItext;
    [SerializeField] private string myTankName;
    private GameObject[] tankArray;
    private TankManager tankManager;
    private string levelNum;
    void Start()
    {
        // The UI element is given the name of the player and searches the tank objects for 
        // the object with the matching name. This is so potentially a dynamic name system
        // could be introduced at some point

        tankArray = GameObject.FindGameObjectsWithTag("Tank");
        foreach(GameObject tank in tankArray)
        {
            if (tank.GetComponent<TankManager>().tankName == myTankName)
            {
                tankManager = tank.GetComponent<TankManager>();
            }
        }
        levelNum = SceneManager.GetActiveScene().buildIndex.ToString();
    }

    private void Update()
    {
        if (!(tankManager == null))
        {
            if (gameObject.CompareTag("HealthUI"))
            {
                UItext.text = "Health " + tankManager.myHealth.ToString();
            }
            else if (gameObject.CompareTag("ScoreUI"))
            {
                UItext.text = "Score: " + tankManager.myScore.ToString();
            }
            else if (gameObject.CompareTag("NameUI"))
            {
                UItext.text = tankManager.tankName.ToString();
            }
            else if (gameObject.CompareTag("TitleUI"))
            {
                UItext.text = "Level " + levelNum;
            }
            else if (gameObject.CompareTag("PowerupUI"))
            {
                UItext.text = "Powerup: " + tankManager.myPowerup;
            }
        }
    }

    


}
