using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score = 0;
    
    public Text scoreText;
    public GameObject gameOverPanel;
    
    public List<GameObject> carrots;


    private void Start()
    {
        gameOverPanel.SetActive(false);
        scoreText.text = 0.ToString();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetScore(int score)
    {
        Score += score;
        scoreText.text = Score.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Score = 0;
        scoreText.text = 0.ToString();
    }

    public void RestartGame()
    {
        
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        
        foreach (GameObject carrot in carrots)
        {
            carrot.SetActive(true);
        }
    }

    public void AddCarrot(GameObject carrot)
    {
        carrots.Add(carrot);
    }
}
