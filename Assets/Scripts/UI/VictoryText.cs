using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI UItext;

    private int player1score, player2score;

    // Start is called before the first frame update
    void Start()
    {
        player1score = MainManager.Instance.score1;
        player2score = MainManager.Instance.score2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Winner() == "both") { UItext.text = "Draw! \n"; }
        else { UItext.text = "Player " + Winner() + " wins! \n" ; }

        UItext.text += "Player 1 score: " + player1score + "\n";
        UItext.text += "Player 2 score: " + player2score;

    }


    private string Winner()
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
