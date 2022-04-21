using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoresMenu : MonoBehaviour
{

    private int winnerScore;


    public void Continue()
    {
        MainManager.Instance.resetScore();
        SceneManager.LoadScene("MainMenu");
    }


}
