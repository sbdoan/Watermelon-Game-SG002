using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Elements")]
    private GameState gameState;

    [Header("Actions")]
    public static Action<GameState> onGameStateChange;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        SetMenu();
    }

    
    void Update()
    {
        
    }

    private void SetMenu()
    {
        GameStateChange(GameState.Menu);
        
    }

    private void SetGame()
    {
        GameStateChange(GameState.Game);
    }

    private void SetGameOver()
    {
        GameStateChange(GameState.GameOver);
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void SetGameState()
    {
        SetGame();
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }

    public void SetGameOverState()
    {
        SetGameOver();
    }

    public void GameStateChange(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChange?.Invoke(gameState);
    }
}
