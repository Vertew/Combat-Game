using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{

    // Class for managing achievements.
    // Not sure this is the best way of doing it but it works well enough.

    public GameObject achievementNotification;
    public GameObject achievementTitle;
    public GameObject achievementDescription;

    public static readonly int achievementNum = 5;
    public static int[] achievementCount = new int[achievementNum];
    private int[] achievementCode = new int[achievementNum];
    private int[] achievementTrigger = new int[achievementNum];

    private void Start()
    {
        SetAchievementValues();
    }
    void Update()
    {
        CheckAchievementState();
    }
    private void CheckAchievementState()
    {
        for (int i = 0; i < achievementNum; i++)
        {
            int j = i + 1;
            achievementCode[i] = PlayerPrefs.GetInt("A0" + j);
            if (achievementCount[i] == achievementTrigger[i] && achievementCode[i] != j)
            {
                StartCoroutine(TriggerAchievement(i, j));
            }
        }
    }
    IEnumerator TriggerAchievement(int i, int j)
    {
        // The achievement code could really be set to anything aside from 0
        // since it's essentially acting as boolean, but a boolean can't be
        // stored in player prefs.
        achievementCode[i] = j;
        PlayerPrefs.SetInt("A0" + j, achievementCode[i]);
        SetAchievementField(PlayerPrefs.GetString("A0" + j + "title"), PlayerPrefs.GetString("A0" + j + "body"));
        yield return new WaitForSeconds(10);
        achievementNotification.SetActive(false);
    }

    private void SetAchievementField(string titleText, string bodyText)
    {
        achievementTitle.GetComponent<Text>().text = titleText;
        achievementDescription.GetComponent<Text>().text = bodyText;
        achievementNotification.SetActive(true);
    }


    private void SetAchievementValues()
    {
        achievementTrigger[0] = 1;
        achievementTrigger[1] = 1;
        achievementTrigger[2] = 1;
        achievementTrigger[3] = 1650;
        achievementTrigger[4] = 1;

        PlayerPrefs.SetString("A01title", "Take that!");
        PlayerPrefs.SetString("A01body", "Hit an enemy for the first time");
        PlayerPrefs.SetString("A02title", "Lotsa points");
        PlayerPrefs.SetString("A02body", "Earn 390 points");
        PlayerPrefs.SetString("A03title", "Victory!");
        PlayerPrefs.SetString("A03body", "Win a match");
        PlayerPrefs.SetString("A04title", "Domination");
        PlayerPrefs.SetString("A04body", "Get the maximum number of points");
        PlayerPrefs.SetString("A05title", "Flawless");
        PlayerPrefs.SetString("A05body", "Finish a level without being hit");
    }


}
