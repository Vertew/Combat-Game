using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinnerScore : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI UItext;

    private void Start()
    {
        UItext.text = "Winner score: " + PlayerPrefs.GetInt("winner");
    }

}
