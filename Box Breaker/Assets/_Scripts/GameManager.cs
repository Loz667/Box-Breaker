using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

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

    private void Start()
    {
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
    }

    IEnumerator SpawnTarget()
    {
        while (true)
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
