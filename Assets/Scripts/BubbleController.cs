using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public bool isMoving;
    public bool isMerging;

    public List<Transform> mergingList = new List<Transform>();

    public BubbleData selfBubbleData;
    private void OnEnable()
    {
        selfBubbleData = GetComponent<BubbleData>();
    }

    private void Update()
    {
        if (selfBubbleData.neighbours.Count > 0 && !isMerging)
        {
            foreach (var neighbour in selfBubbleData.neighbours)
            {
                if(neighbour!=null)
                    Debug.Log("BubblecontrollerNeighburs");
            }

            isMerging = true;
        }
    }

    public void MoveBubble(Transform bubble,Vector3 direction, float shootSpeed)
    {
        transform.Translate(direction * (shootSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GetComponent<Collider2D>().enabled = false;
        var colBubbleData = col.GetComponent<BubbleData>();
        if (colBubbleData)
        {
            isMoving = false;
            if (colBubbleData.number == selfBubbleData.number)
            {
                isMerging = true;
                colBubbleData.AddMergingList(selfBubbleData.number,mergingList);
                var uniqeMergeList = mergingList.Distinct().ToList();
                MergeBubblesInList(uniqeMergeList);
            }
            else
            {
                this.enabled = false;
            }
        }
    }

    private void MergeBubblesInList(List<Transform> _mergingList)
    {
        var farBubble = _mergingList[^1];

        var distanceMax = Vector3.Distance(transform.position, farBubble.position);
        
        GameManager.Instance.Merge(selfBubbleData,_mergingList[^1]);

        foreach (var neighbour in _mergingList)
        {
            var distanceTemp = Vector3.Distance(transform.position, neighbour.position);
            if (distanceTemp > distanceMax)
            {
                farBubble = neighbour;
                distanceMax = distanceTemp;
            }
        }

        farBubble.tag = "TargetLast";
        foreach (var newNeighbours in farBubble.GetComponent<BubbleData>().neighbours)
        {
            selfBubbleData.neighbours.Add(newNeighbours);
        }
        foreach (var mergeTarget in _mergingList)
        {
            mergeTarget.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("Merge others");
            GameManager.Instance.Merge(mergeTarget.GetComponent<BubbleData>(),farBubble);
            mergeTarget.GetComponent<BubbleData>().ExplodeSelf();
        }

        selfBubbleData.UpdateExponent(_mergingList.Count);
    }

    public void ControlNeighbours()
    {
        foreach (var neighbour in selfBubbleData.neighbours)
        {
            if (neighbour == null)
            {
                selfBubbleData.neighbours.Remove(neighbour);
            }
        }
        mergingList.Clear();
        mergingList = new List<Transform>();
        selfBubbleData.AddMergingList(selfBubbleData.number,mergingList);
        if(mergingList.Count <= 0)
        {
            GameManager.Instance.MakeBubbleLine();
            enabled = false;
            return;
        }
        
        var uniqeMergeList = mergingList.Distinct().ToList();
        MergeBubblesInList(uniqeMergeList);
    }
}
