using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MergeCommand : Command
{
    private Transform targetPos;
    private BubbleData bubble;

    public MergeCommand(BubbleData _bubble, Transform _target)
    {
        bubble = _bubble;
        targetPos = _target;
    }
    protected override async Task AsyncExecute()
    {
        if (bubble == null || targetPos == null)
        {
            return;
        }
        while (bubble.transform.position != targetPos.position)
        {
            bubble.transform.position = Vector3.MoveTowards(bubble.transform.position, targetPos.position, 0.2f);
            await Task.Delay(20);
        }
        if (targetPos.CompareTag("TargetLast"))
        {
            GameManager.Instance.currShootingBubble.ControlNeighbours();
        }
    }
}
