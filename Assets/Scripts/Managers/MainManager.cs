using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This is a singleton class!! You're using the singleton pattern here :sunglasses:
public class MainManager : MonoBehaviour
{

    public static MainManager Instance;

    // The scores of the two players are stored here in the singleton class
    public int score1, score2;

    void Start()
    {
        GameEvents.current.OnTankKilled += OnKilled;
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

    public void updateScore(int score, string name)
    {

        if(name == "TankPlayer")
        {
            score1 = score;
        }
        else
        {
            score2 = score;
        }

    }


}
