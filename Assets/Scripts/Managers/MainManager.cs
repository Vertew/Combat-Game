using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This is a singleton class!! You're using the singleton pattern here :sunglasses:
public class MainManager : MonoBehaviour
{

    public static MainManager Instance;

    // The scores of the two players are stored here in the singleton class while the game is in progress
    public int score1, score2, currentLevel;
    public bool singleplayer, scoreSaved;
    private GameObject playerAI, playerHuman;
    [SerializeField] private GameObject powerups;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        GameEvents.current.OnTankKilled += OnKilled;
        GameEvents.current.OnTankHit += OnRoundRestart;
    }

    private void Awake()
    {

        score1 = 0;
        score2 = 0;

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // When a tank is killed, the next scene/level is loaded.
    private void OnKilled()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnRoundRestart()
    {
        for (int i = 0; i < powerups.transform.childCount; i++)
        {
            powerups.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void updateSingleplayer(bool val)
    {
        singleplayer = val;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        powerups = GameObject.Find("Powerups");
        if (currentLevel > 0 && currentLevel < 6)
        {
            playerAI = GameObject.Find("TankEnemy");
            playerHuman = GameObject.Find("TankPlayer2");
            if (singleplayer)
            {
                playerHuman.SetActive(false);
            }
            else
            {
                playerAI.SetActive(false);
            }
        }
    }

    public void UpdateScore(int score, string name)
    {

        if(name == "TankPlayer")
        {
            score1 = score;
            AchievementManager.achievement02Count = score;
        }
        else if (name == "TankPlayer2")
        {
            score2 = score;
            AchievementManager.achievement02Count = score;
        }
        else
        {
            // Unfortunately for the computer, it doesn't get to earn achievements.
            score2 = score;
        }

    }

    public void ResetScore()
    {
        score1 = 0;
        score2 = 0;
        PlayerPrefs.SetInt("winner", 0);
        scoreSaved = false;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
