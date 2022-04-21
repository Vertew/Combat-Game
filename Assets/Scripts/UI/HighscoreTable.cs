using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private List<HighscoreEntry> highscoreEntryList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntry");

        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{score = 0, name = "joe" }
        };

        string storedJson = PlayerPrefs.GetString("highscoreTable");

        if (storedJson == "")
        {
            storedJson = JsonUtility.ToJson(highscoreEntryList);
        }

        Highscores highscores = JsonUtility.FromJson<Highscores>(storedJson);

        CreateTable(highscores);

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 70f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;

        entryTransform.Find("rankText").GetComponent<Text>().text = rank.ToString();

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;

        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);

    }

    private void CreateTable(Highscores highscores)
    {

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        if (highscores.highscoreEntryList.Count > 10)
        {
            highscores.highscoreEntryList.RemoveRange(10, highscores.highscoreEntryList.Count - 10);
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry entry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(entry, entryContainer, highscoreEntryTransformList);
        }

    }

    public void AddHighscoreEntry()
    {
        int newScore = PlayerPrefs.GetInt("winner");

        string newName = gameObject.GetComponentInChildren<InputField>().text;

        HighscoreEntry highscoreEntry = new HighscoreEntry { score = newScore, name = newName };

        string storedJson = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(storedJson);

        highscores.highscoreEntryList.Add(highscoreEntry);

        CreateTable(highscores);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        gameObject.GetComponentInChildren<InputField>().enabled = false;
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }


    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }


}
