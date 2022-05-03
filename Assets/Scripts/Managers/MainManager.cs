using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    // The main manager class handles general aspects of the game such as
    // loading scenes, restarting rounds and storing game values such as
    // whether or not the game is in singleplayer mode, player scores etc.
    // It is also an implementation of the singleton pattern.

    public static MainManager Instance;

    // The scores of the two players are stored here in the singleton class while the game is in progress
    public int score1, score2, currentLevel;
    public bool singleplayer, scoreSaved, fromMain, flawless;
    public string levelLoser;
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

    public void UpdateSingleplayer(bool val)
    {
        singleplayer = val;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        powerups = GameObject.Find("Powerups");
        if (currentLevel > 0 && currentLevel < 6)   
        {
            if (score1 >= 390){AchievementManager.achievementCount[1] = 1;}
            if (!singleplayer)
            {
                GameObject.Find("TankEnemy").SetActive(false);
                if (score2 >= 390) {AchievementManager.achievementCount[1] = 1;}
            }
            else
            {
                GameObject.Find("TankPlayer2").SetActive(false);
            }
            if (flawless && currentLevel != 1) {AchievementManager.achievementCount[4] = 1;}
        }
    }

    public void UpdateScore(int score, string name)
    {

        if(name == "TankPlayer")
        {
            score1 = score;
            if (levelLoser != name)
            {
                score1 += 100;
            }
        }
        else if (name == "TankPlayer2")
        {
            score2 = score;
            if (levelLoser != name)
            {
                score2 += 100;
            }
        }
        else
        {
            score2 = score;
            if (levelLoser != name)
            {
                score2 += 100;
            }
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
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
