using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI UItext;
    private FinishedMenu finishedMenu;

    // Start is called before the first frame update
    void Start()
    {
        finishedMenu = gameObject.GetComponentInParent<FinishedMenu>();
    }


    // Update is called once per frame
    void Update()
    {
        
        if (finishedMenu.Winner() == "both") { UItext.text = "Draw! \n"; }
        else { UItext.text = "Player " + finishedMenu.Winner() + " wins! \n"; }

        UItext.text += "Player 1 score: " + finishedMenu.player1score + "\n";
        UItext.text += "Player 2 score: " + finishedMenu.player2score;
        
    }

}
