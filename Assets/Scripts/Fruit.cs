using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fruit : MonoBehaviour
{
    [Header("Data ")]
    [SerializeField] private FruitType type;
    private bool hasCollided;

    [Header("Actions ")]
    public static Action<Fruit, Fruit> collisionWithFruit;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void ChangePhysicsBodyType()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
    }

    /*public void ChangePosition(Vector2 targetposition)
    {
        transform.position = targetposition;
    }*/

    public FruitType GetFruitType()
    {
        return type;
    } 

    public bool HasCollided()
    {
        return hasCollided;
    }


    void OnCollisionEnter2D(Collision2D collison)
    {
        hasCollided = true;
        if(collison.collider.TryGetComponent(out Fruit otherFruit))
        {
            //Destroy(fruit.gameObject);
            if(otherFruit.GetFruitType() != type)
                return;

            collisionWithFruit?.Invoke(this, otherFruit);

        }

    }
}
