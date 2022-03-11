using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    private int score = 0;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        GameEvents.current.onTankHit += OnScoreUpdate;
    }

    private void Update()
    {
        scoreText.text = score.ToString();

    }

    private void OnScoreUpdate()
    {
        score += 1;
    }


}
