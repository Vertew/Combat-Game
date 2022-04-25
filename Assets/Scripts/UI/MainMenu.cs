using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject singleplayerButton;
    [SerializeField] private GameObject multiplayerButton;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject scoresButton;
    [SerializeField] private GameObject achievementsButton;

    void Start()
    {
        MainManager.Instance.ResetScore();
    }

    public void StartGame()
    {
        singleplayerButton.SetActive(true);
        multiplayerButton.SetActive(true);
        startButton.SetActive(false);
        scoresButton.SetActive(false);
        achievementsButton.SetActive(false);
    }

    public void StartSingleplayer()
    {
       MainManager.Instance.UpdateSingleplayer(true);
       SceneManager.LoadScene(1);
    }

    public void StartMultiplayer()
    {
        MainManager.Instance.UpdateSingleplayer(false);
        SceneManager.LoadScene(1);
    }

    public void ViewScores()
    {
        MainManager.Instance.fromMain = true;
        SceneManager.LoadScene(7);
    }

    public void ViewAchievements()
    {
        SceneManager.LoadScene(8);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting...");
    }
}
