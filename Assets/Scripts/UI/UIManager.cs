using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text UItext;
    [SerializeField] private GameObject tankObj;
    private TankManager tankManager;
    void Start()
    {
        tankManager = tankObj.GetComponent<TankManager>();
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
    }

}
