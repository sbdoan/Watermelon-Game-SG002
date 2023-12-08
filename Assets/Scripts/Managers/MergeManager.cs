using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//

public class MergeManager : MonoBehaviour
{
    [Header ("Elements ")]
    Fruit lastSender;

    [Header("Actions ")]
    public static Action<FruitType, Vector2> mergeSecondStep;

    void Awake()
    {
        Fruit.collisionWithFruit += CollisionWithFruitCallback;
    }

    void OnDestroy()
    {
        Fruit.collisionWithFruit -= CollisionWithFruitCallback;
    }


    void Update()
    {
        
    }

    private void  CollisionWithFruitCallback(Fruit sender, Fruit otherFruit)
    {
        if (lastSender != null)
            return;

        lastSender = sender;
        Merge(sender, otherFruit);
        /*GetMergeFruitType(sender, otherFruit);
        Debug.Log(GetMergeFruitType(sender, otherFruit));*/
        Debug.Log("Collision by " + sender.name);

    }
    
    /*public FruitType GetMergeFruitType(Fruit sender, Fruit otherFruit)
    {
        FruitType mergeType = sender.GetFruitType();
        mergeType += 1;

        return mergeType;
    }*/
    private void Merge(Fruit sender, Fruit otherFruit)
    {
        FruitType mergeType = sender.GetFruitType();
        mergeType += 1;


        Vector2 spawnPosition = (sender.transform.position + otherFruit.transform.position)/2;

        Destroy(sender.gameObject);
        Destroy(otherFruit.gameObject);

        StartCoroutine(LastSenderWaitCoroutine());

        mergeSecondStep?.Invoke(mergeType, spawnPosition);

    }



    IEnumerator LastSenderWaitCoroutine()
    {
        yield return new WaitForEndOfFrame();
        lastSender = null;
    }
    
}
