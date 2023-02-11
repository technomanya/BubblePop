using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player;
    public BubbleController currShootingBubble;
    
    public Command activeCommand;
    private Queue<Command> commandQueue = new Queue<Command>();

    public int maxExponent;
    public bool isMerging = false;

    [SerializeField] private LevelMaker levelMaker;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currShootingBubble = player.currentBubble.GetComponent<BubbleController>();
        activeCommand = new MergeCommand(currShootingBubble.selfBubbleData,currShootingBubble.transform);
        levelMaker.PlaceRandomMultiBubbles();
    }

    private void Update()
    {
        if (!activeCommand.isExecuting && commandQueue.Count > 0)
        {
            activeCommand = commandQueue.Dequeue();
            activeCommand.Execute();
        }
        else
        {
            currShootingBubble.isMerging = false;
        }
    }

    public void Merge(BubbleData mergingBubble,Transform target)
    {
        commandQueue.Enqueue(new MergeCommand(mergingBubble,target));
    }

    public void MakeBubbleLine()
    {
        levelMaker.minExponent++;
        levelMaker.maxExponent++;
        levelMaker.bubbleAmount = 10;
        levelMaker.PlaceRandomMultiBubbles();
    }
}
