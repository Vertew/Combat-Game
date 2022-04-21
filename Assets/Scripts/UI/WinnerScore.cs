using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinnerScore : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI UItext;

    void Update()
    {
        UItext.text = "Winner score: " + PlayerPrefs.GetInt("winner");
    }

}
