using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool isGameActive;

    public GameObject startScreen;
    public GameObject highScoresTable;
    public GameObject gameOverScreen;

    public TextMeshProUGUI scoreText;

    public List<GameObject> targets;
    
    private int score;

    private float spawnRate = 1.0f;

    public static GameManager Instance
    {
        get 
        {
            if (_instance == null)
                Debug.Log("No Game Manager exists");

            return _instance; 
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void StartGame()
    {
        isGameActive = true;
        startScreen.gameObject.SetActive(false);
        StartCoroutine(SpawnTarget());
        scoreText.gameObject.SetActive(true);
        score = 0;
        UpdateScore(0);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.gameObject.SetActive(true);
        highScoresTable.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
