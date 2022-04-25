using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresMenu : MonoBehaviour
{

    public bool scoreSaved;
    private InputField scoreInput;

    public void Start()
    {
        scoreInput = gameObject.GetComponentInChildren<InputField>();
    }

    public void Continue()
    {
        MainManager.Instance.fromMain = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (MainManager.Instance.scoreSaved || MainManager.Instance.fromMain == true)
        {
            scoreInput.enabled = false;
        }
    }

}
