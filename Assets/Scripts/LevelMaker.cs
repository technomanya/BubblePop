using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelMaker : MonoBehaviour
{
    public List<GameObject> bubbleInstanceList;
    public BubbleData bubblePrefab;
    public List<GameObject> bubblePrefabList;
    public float rightBorder, leftBorder, ceilingY;
    public Vector3 spawnPoint ;
    
    [HideInInspector]
    public int bubbleAmount;
    [HideInInspector]
    public int currBubbleNumber;
    [HideInInspector] 
    public int minExponent, maxExponent;
    
    public void PlaceBubble()
    {
        var bubbleInstance=Instantiate(bubblePrefab, spawnPoint, Quaternion.identity);
        bubbleInstance.SetBubbleProperties(currBubbleNumber);
        bubbleInstanceList.Add(bubbleInstance.gameObject);
        if(spawnPoint.x + 1 <= rightBorder)
            spawnPoint += Vector3.right;
        else
        {
            spawnPoint += Vector3.down;
            spawnPoint = new Vector3(leftBorder, spawnPoint.y, 0);
        }
    }

    public void PlaceMultiBubbles()
    {
        for (int i = 0; i < bubbleAmount; i++)
        {
            PlaceBubble();
        }
    }

    public void PlaceRandomMultiBubbles()
    {
        var numberList = new [] { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };
        for (var i = 0; i < bubbleAmount; i++)
        {
            var numIndex = Random.Range(minExponent-1, maxExponent);
            currBubbleNumber = numberList[numIndex];
            PlaceBubble();
        }
    }

    public void ResetLevelScreen()
    {
        foreach (var bubble in bubbleInstanceList)
        {
            DestroyImmediate(bubble);
        }
        bubbleInstanceList.Clear();
        bubbleInstanceList = new List<GameObject>();
        spawnPoint = new Vector3(leftBorder, ceilingY, 0);
    }
}
