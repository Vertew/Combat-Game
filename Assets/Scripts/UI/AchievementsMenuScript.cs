using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementsMenuScript : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetAchievements()
    {
        PlayerPrefs.SetInt("A01", 0);
        PlayerPrefs.SetInt("A02", 0);
        PlayerPrefs.SetInt("A03", 0);
        PlayerPrefs.SetInt("A04", 0);
        PlayerPrefs.SetInt("A05", 0);
        SceneManager.LoadScene("AchievementsMenu");
    }
}
