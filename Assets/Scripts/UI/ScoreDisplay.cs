using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    private int score;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        GameEvents.current.OnTankHit += OnScoreUpdate;
    }

    private void Update()
    {
        scoreText.text = score.ToString();

    }

    public void UpdateHP() { 
    }

    private void OnScoreUpdate()
    {
        score += 1;
    }


}
