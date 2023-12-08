using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(FruitManager))]
public class UI : MonoBehaviour
{
    [Header ("Elements")]
    [SerializeField] private Image nextImage;
    private FruitManager fruitManager;

    void Awake()
    {
        FruitManager.onNextFruitIndexSet += UpdateNextFruitImage;
    }

    void OnDestroy()
    {
        FruitManager.onNextFruitIndexSet -= UpdateNextFruitImage;
    }
    void Start()
    {
        fruitManager = GetComponent<FruitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void UpdateNextFruitImage()
    {
        nextImage.sprite = fruitManager.GetNextFruitImage();
    }


}
