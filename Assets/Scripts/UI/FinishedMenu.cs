using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedMenu : MonoBehaviour
{

    public int player1score, player2score, winnerScore;

    // The scores of the game that just finished are kept and the scores are then
    // reset in main manager back to 0.
    void Start()
    {
        player1score = MainManager.Instance.score1;
        player2score = MainManager.Instance.score2;
        AllocateBonus();
    }

    public void Scores()
    {
        SceneManager.LoadScene("ScoresMenu");
    }

    public void Continue()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting...");
    }

    private void AllocateBonus()
    {
        if (Winner() == "1")
        {
            winnerScore = player1score += 1000;
            PlayerPrefs.SetInt("winner", winnerScore);
            AchievementManager.achievementCount[2] = 1;
            AchievementManager.achievementCount[3] = winnerScore;
        }
        else if (Winner() == "2")
        {
            winnerScore = player2score += 1000;
            PlayerPrefs.SetInt("winner", winnerScore);
            if (!MainManager.Instance.singleplayer)
            {
                AchievementManager.achievementCount[2] = 1;
                AchievementManager.achievementCount[3] = winnerScore;
            }
        }
        else
        {
            winnerScore = player1score += 500;
            PlayerPrefs.SetInt("winner", winnerScore);
        }
    }

    public string Winner()
    {
        if (player1score > player2score)
        {
            return "1";
        }
        else if (player1score < player2score)
        {
            return "2";
        }
        else
        {
            return "both";
        }
    }
}
