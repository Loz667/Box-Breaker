using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private List<Transform> highscoreTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreContainer");
        entryTemplate = entryContainer.Find("HighScoreEntry");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        //Sort list
        for (int i = 0; i < highScores.highscoreEntries.Count; i++)
        {
            for (int j = i + 1; j < highScores.highscoreEntries.Count; j++)
            {
                if (highScores.highscoreEntries[j].score > highScores.highscoreEntries[i].score)
                {
                    HighscoreEntry tmp = highScores.highscoreEntries[i];
                    highScores.highscoreEntries[i] = highScores.highscoreEntries[j];
                    highScores.highscoreEntries[j] = tmp;
                }
            }
        }

        if (highScores.highscoreEntries.Count > 10)
        {
            for (int h = highScores.highscoreEntries.Count; h > 10; h--)
            {
                highScores.highscoreEntries.RemoveAt(10);
            }
        }

        highscoreTransformList = new List<Transform>();
        foreach (HighscoreEntry entry in highScores.highscoreEntries)
        {
            CreateHighScoreEntry(entry, entryContainer, highscoreTransformList);
        }
    }

    void CreateHighScoreEntry(HighscoreEntry scoreEntry, Transform container, List<Transform> scoreList)
    {
        float entryHeight = 37.5f;
        Transform newEntry = Instantiate(entryTemplate, container);
        RectTransform newEntryRect = newEntry.GetComponent<RectTransform>();
        newEntryRect.anchoredPosition = new Vector2(0f, -entryHeight * scoreList.Count);
        newEntry.gameObject.SetActive(true);

        int rank = scoreList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        newEntry.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = scoreEntry.score;
        newEntry.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = scoreEntry.name;
        newEntry.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        newEntry.Find("Background").gameObject.SetActive(rank % 2 == 1);
        scoreList.Add(newEntry);
    }

    private void AddHighScoreEntry(int _score, string _name)
    {
        HighscoreEntry entry = new HighscoreEntry { score = _score, name = _name };

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        highScores.highscoreEntries.Add(entry);

        for (int i = 0; i < highScores.highscoreEntries.Count; i++)
        {
            for (int j = i + 1; j < highScores.highscoreEntries.Count; j++)
            {
                if (highScores.highscoreEntries[j].score > highScores.highscoreEntries[i].score)
                {
                    HighscoreEntry tmp = highScores.highscoreEntries[i];
                    highScores.highscoreEntries[i] = highScores.highscoreEntries[j];
                    highScores.highscoreEntries[j] = tmp;
                }
            }
        }

        if (highScores.highscoreEntries.Count > 10)
        {
            for (int h = highScores.highscoreEntries.Count; h > 10; h--)
            {
                highScores.highscoreEntries.RemoveAt(10);
            }
        }

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<HighscoreEntry> highscoreEntries;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
