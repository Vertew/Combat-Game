using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text UItext;
    [SerializeField] private GameObject tankObj;
    private TankManager tankManager;
    private string levelNum;
    void Start()
    {
        tankManager = tankObj.GetComponent<TankManager>();
        levelNum = SceneManager.GetActiveScene().buildIndex.ToString();
    }

    private void Update()
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
    }

    


}
