using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsContainerScript : MonoBehaviour
{
    private int achievementCode;

    // Very basic method for collecting the player prefs data storing achievements

    void Start()
    {
        int i = 1;
        
        foreach (Transform child in transform)
        {
            achievementCode = PlayerPrefs.GetInt("A0" + i);
            if (achievementCode == i)
            {
                bool j = false;
                foreach (Transform textField in child)
                {
                    if (j)
                    {
                        textField.GetComponent<Text>().text = PlayerPrefs.GetString("A0" + i + "body");
                    }
                    else
                    {
                        textField.GetComponent<Text>().text = PlayerPrefs.GetString("A0" + i + "title");
                    }
                    j = true;
                }
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
