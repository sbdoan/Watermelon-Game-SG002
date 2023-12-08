using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header ("Elements ")]
    [SerializeField] private GameObject gameOverLine;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private float timeThreshold;
    private float timer;
    private bool timerBool;
    private bool gameOverBool;

    void Start()
    {
        
    }
        void Update()
    {
        if(!gameOverBool)
            ManageGameOver();
    }

    private void ManageGameOver()
    {
        if(timerBool)
            ManageTimerOn();
        else
        {
            if(CheckIfFruitAboveLine())
                StartTimer();
        }
    }

    private void ManageTimerOn()
    {
        timer += Time.deltaTime;

            if(!CheckIfFruitAboveLine())
                StopTimer();

            if(timer >= timeThreshold)
            {
                GameOver();
            }
    }
    private bool CheckIfFruitAboveLine()
    {
        for (int i = 0 ; i < spawnParent.childCount; i++)
        {
            Fruit fruit = spawnParent.GetChild(i).GetComponent<Fruit>();
            if(!fruit.HasCollided())
                continue;

            if(CheckIfFruitAboveLine(spawnParent.GetChild(i)))
                return true;
        }

        return false;
    }

    private bool CheckIfFruitAboveLine(Transform fruit)
    {
        if(fruit.position.y > gameOverLine.transform.position.y)
            return true;

        return false;

    }

    private void StartTimer()
    {
        timer = 0;
        timerBool = true;
    }

    private void StopTimer()
    {
        timerBool = false;
    }
    private void GameOver()
    {
        Debug.LogError("Game Over ");
        gameOverBool = true;

    
        GameManager.instance.SetGameOverState();
    }
    
}
