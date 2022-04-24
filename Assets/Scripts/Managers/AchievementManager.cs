using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{

    // Achievement manager class. Surprisingly, this class handles the management of achievements.
    // Not sure this is the best way of doing this but I haven't got a huge amount of time to find
    // a better way unfortunately and it does the job.

    public GameObject achievementNotification;
    public GameObject achievementTitle;
    public GameObject achievementDescription;

    public static int achievement01Count, achievement02Count, achievement03Count, achievement04Count, achievement05Count;
    private int achievement01Code, achievement02Code, achievement03Code, achievement04Code, achievement05Code;
    private int achievement01Trigger = 1;
    private int achievement02Trigger = 210;

    private void Start()
    {
        // Resetting prefs for testing purposes
        PlayerPrefs.SetInt("A01", 0);
        PlayerPrefs.SetInt("A02", 0);
    }
    void Update()
    {
        achievement01Code = PlayerPrefs.GetInt("A01");
        achievement02Code = PlayerPrefs.GetInt("A02");
        if (achievement01Count == achievement01Trigger && achievement01Code != 01)
        {
            StartCoroutine(TriggerAchievement01());
        }
        if (achievement02Count == achievement02Trigger && achievement02Code != 02)
        {
            StartCoroutine(TriggerAchievement02());
        }
    }

    IEnumerator TriggerAchievement01()
    {
        // The achievement code could really be set to anything aside from 0
        // since it's essentially acting as boolean, but a boolean can't be
        // stored in player prefs. I set it to the achievement number for clarity.
        achievement01Code = 01;
        PlayerPrefs.SetInt("A01", achievement01Code);
        AchievementTextSetter("My First Hit!", "Hit an enemy for the first time");
        yield return new WaitForSeconds(10);
        achievementNotification.SetActive(false);
    }

    IEnumerator TriggerAchievement02()
    {
        achievement02Code = 02;
        PlayerPrefs.SetInt("A02", achievement02Code);
        AchievementTextSetter("Lotsa points", "Get a load of points");
        yield return new WaitForSeconds(10);
        achievementNotification.SetActive(false);
    }

    private void AchievementTextSetter(string titleText, string bodyText)
    {
        achievementTitle.GetComponent<Text>().text = titleText;
        achievementDescription.GetComponent<Text>().text = bodyText;
        achievementNotification.SetActive(true);
    }


}
