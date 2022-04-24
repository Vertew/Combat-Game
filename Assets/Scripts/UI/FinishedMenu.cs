using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedMenu : MonoBehaviour
{

    public int player1score, player2score;

    // The scores of the game that just finished are kept and the scores are then
    // resent in main manager back to 0.
    void Start()
    {
        player1score = MainManager.Instance.score1;
        player2score = MainManager.Instance.score2;
    }

    void Update()
    {
        
    }

    public void Scores()
    {
        SceneManager.LoadScene("ScoresMenu");
    }

    public void Continue()
    {
        //MainManager.Instance.ResetScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting...");
    }

    public string Winner()
    {
        if (player1score > player2score)
        {
            PlayerPrefs.SetInt("winner", player1score);
            return "1";
        }
        else if (player1score < player2score)
        {
            PlayerPrefs.SetInt("winner", player2score);
            return "2";
        }
        else
        {
            PlayerPrefs.SetInt("winner", player1score);
            return "both";
        }
    }

}
