using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [Header ("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        GameManager.onGameStateChange += GameStateChangedCall;
        DontDestroyOnLoad(gameObject);
    }
    void OnDestroy()
    {
        GameManager.onGameStateChange -= GameStateChangedCall;
    }
    void Start()
    {
    
    }
    void Update()
    {
        
    }

    private void GameStateChangedCall(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                SetMenu();
                break;
            case GameState.Game:
                SetGame();
                break;
            case GameState.GameOver:
                SetGameOver();
                break;
        }
    }

    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        
    }

        private void SetGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        
    }

        private void SetGameOver()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        
    }

    public void PlayButtonCall()
    {
        GameManager.instance.SetGameState();
        SetGame();        
    }

    public void NextButtonCall()
    {
        SceneManager.LoadScene(0);
    }
}
