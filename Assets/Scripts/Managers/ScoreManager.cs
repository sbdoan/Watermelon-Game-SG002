using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header ("Elements ")]
    [SerializeField] private TextMeshProUGUI regularScoreText;
    [SerializeField] private TextMeshProUGUI watermelonScoreText;

    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [Header("Data ")]
    private int score;
    private int wScore;

    private int bestScore;

    private const string bestScoreKey = "bestScoreKey";

    void Awake()
    {
        LoadData();
        MergeManager.mergeSecondStep += MergeProcessCallback;
        GameManager.onGameStateChange += GameStateChangeCall;
    }

    void OnDestroy()
    {
        MergeManager.mergeSecondStep -= MergeProcessCallback;
        GameManager.onGameStateChange -= GameStateChangeCall;
    }
    void Start()
    {
        UpdateScore();
        UpdateWatermelonScore();
        UpdateBestScore();
        CalculateBestScore();
    }

    
    void Update()
    {
        
        
    }

    private void GameStateChangeCall(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GameOver:
                CalculateBestScore();
                break;
        }
    }


    private void MergeProcessCallback(FruitType fruitType, Vector2 unnecessaryVector)
    {
        
        
        int scoreToAdd = (int)Mathf.Pow(2, ((int)fruitType + 1));
        score += scoreToAdd;
        UpdateScore();
        if((int)fruitType == 10)
        {
            wScore ++;
            UpdateWatermelonScore();
        }
 
    }

    private void UpdateScore()
    {
        regularScoreText.text = score.ToString();
        gameOverScoreText.text = "SCORE: " + score.ToString();
        
    }

    private void UpdateBestScore()
    {
        bestScoreText.text = "BEST SCORE: " + bestScore.ToString();
    }

    private void UpdateWatermelonScore()
    {
        watermelonScoreText.text = wScore.ToString();
    }




    private void CalculateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            SaveData();
        }
    }

    private void LoadData()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(bestScoreKey, bestScore);
    }

}
