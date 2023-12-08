using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class FruitManager : MonoBehaviour
{

    [Header("Elements ")]

    [SerializeField] private Transform spawnParent;
    [SerializeField] private Fruit[] fruitPrefabs;
    [SerializeField] private Fruit[] spawnableFruits;
    [SerializeField] private LineRenderer dropTrailLine;
    private Fruit spawnedFruit;

    [Header("Settings ")]
    [SerializeField] private Transform fruitYPosition;
    private int nextFruitIndex;

    [Header("Debug ")]
    [SerializeField] private bool enableGizmos;

    [Header("Actions ")]
    public static Action onNextFruitIndexSet;

    void Awake()
    {
        MergeManager.mergeSecondStep += MergeCallback;
    }

    void OnDestroy()
    {
        MergeManager.mergeSecondStep -= MergeCallback;
    }
    void Start()
    {
        HideDropLine();
    }

    
    void Update()
    {
        if(!GameManager.instance.IsGameState())
            return;

        ManagePlayerInput();
    }

    private void ManagePlayerInput()
    {
        if(Input.GetMouseButtonDown(0))
            MouseDownCallback();
        else if(Input.GetMouseButton(0))
            MouseDragCallback();
        else if(Input.GetMouseButtonUp(0))
            MouseUpCallback();



    }
    private void MouseDownCallback()
    {
        ShowDropLine();
        LinePosition();
        SpawnFruit();
    }
    private void MouseDragCallback()
    {
        LinePosition();
        spawnedFruit.transform.position = GetSpawnPosition(GetClickedWorldPosition());
    }
    private void MouseUpCallback()
    {
        HideDropLine();
        if(spawnedFruit != null)
            spawnedFruit.ChangePhysicsBodyType();
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition(GetClickedWorldPosition());        
        spawnedFruit = Instantiate(spawnableFruits[nextFruitIndex], spawnPosition, Quaternion.identity,spawnParent);

        spawnedFruit.name = "Fruit_ " + Random.Range(0, 100);
        SetNextFruitIndex();
        onNextFruitIndexSet?.Invoke();
    }

    private void SetNextFruitIndex()
    {
        nextFruitIndex = Random.Range(0, spawnableFruits.Length);
    }
    public string GetNextFruitName()
    {
        return spawnableFruits[nextFruitIndex].name;
    }

    public Sprite GetNextFruitImage()
    {
        return spawnableFruits[nextFruitIndex].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
    

    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private Vector2 GetSpawnPosition(Vector2 clickedWorldPosition)
    {
        clickedWorldPosition.y = fruitYPosition.position.y;
        return clickedWorldPosition;
    }

    private void LinePosition()
    {
        dropTrailLine.SetPosition(0, GetSpawnPosition(GetClickedWorldPosition()));
        dropTrailLine.SetPosition(1, GetSpawnPosition(GetClickedWorldPosition()) + Vector2.down*15);
    }
    private void HideDropLine()
    {
        dropTrailLine.enabled = false;
    }

    private void ShowDropLine()
    {
        dropTrailLine.enabled = true;
    }

    private void MergeCallback(FruitType fruitType, Vector2 spawnPosition)
    {
        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            if (fruitPrefabs[i].GetFruitType() == fruitType)
            {
                SpawnMergeFruit(fruitPrefabs[i], spawnPosition);
                break;
            }
                
        }
    }

    private void SpawnMergeFruit(Fruit fruit, Vector2 spawnPosition)
    {
        Fruit mergeFruit = Instantiate(fruit, spawnPosition, Quaternion.identity, spawnParent);
        mergeFruit.ChangePhysicsBodyType();
        Debug.Log(mergeFruit);
        if(mergeFruit.GetFruitType() == FruitType.watermelon)
        {
            StartCoroutine(WaitSecondsCoroutine(mergeFruit));
            //destroy watermelon
        }
    }

    private IEnumerator WaitSecondsCoroutine(Fruit mergeFruit)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Waited");
        Destroy(mergeFruit.gameObject);
        
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!enableGizmos)
            return;

        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector3 (-50, fruitYPosition.position.y, 0), new Vector3 (50, fruitYPosition.position.y, 0));
    }
#endif
}
