using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string level1;

    [SerializeField] private GameObject singleplayerButton;
    [SerializeField] private GameObject multiplayerButton;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject optionsButton;

    // Start is called before the first frame update
    void Start()
    {
        MainManager.Instance.ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        singleplayerButton.SetActive(true);
        multiplayerButton.SetActive(true);
        startButton.SetActive(false);
        optionsButton.SetActive(false);
    }

    public void StartSingleplayer()
    {
       MainManager.Instance.updateSingleplayer(true);
       SceneManager.LoadScene(level1);
    }

    public void StartMultiplayer()
    {
        MainManager.Instance.updateSingleplayer(false);
        SceneManager.LoadScene(level1);
    }

    public void ViewScores()
    {
        SceneManager.LoadScene(7);
    }

    public void CloseOptions()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting...");
    }


}
