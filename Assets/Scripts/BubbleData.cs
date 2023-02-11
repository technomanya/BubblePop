using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BubbleData : MonoBehaviour
{
    public int exponent;
    [HideInInspector]
    public int number;

    public Transform ceiling;
    public List<BubbleData> neighbours;
    public List<BubbleData> mergeGroup;

    public TMP_Text numberText;

    private void Start()
    {
        FindNeighbour(transform.up);
        FindNeighbour(-transform.up);
        FindNeighbour(transform.right);
        FindNeighbour(-transform.right);
    }

    private void FindNeighbour(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction,1f);
        if (hit.collider != null && hit.collider != GetComponent<Collider2D>())
        {
            if(hit.collider.GetComponent<BubbleData>())
            {
                neighbours.Add(hit.transform.gameObject.GetComponent<BubbleData>());
            }
            else if(hit.transform.root.CompareTag("Ceiling"))
            {
                ceiling = hit.transform;
            }
            //AddJoint(hit.transform.GetComponent<Rigidbody2D>());
        }
    }
    public void SetBubbleProperties(int _number)
    {
        number = _number;
        exponent = (int)Mathf.Log(number, 2);
        numberText.text = number.ToString();
        var mat = Resources.Load(number.ToString()) as Material;
        GetComponent<SpriteRenderer>().material = mat;
    }

    public void AddJoint(Rigidbody2D targetRig)
    {
        var joint = gameObject.AddComponent<DistanceJoint2D>();
        joint.connectedBody = targetRig;
        joint.distance = 1;
    }

    public void AddMergingList(int currNumber,List<Transform> mergingList)
    {
        mergingList.Add(transform);
        foreach (var neighbour in neighbours.Where(vaNeighbour => vaNeighbour.number == currNumber))
        {
            if(!mergingList.Contains(neighbour.transform))
                neighbour.AddMergingList(currNumber,mergingList);
        }
    }

    public void UpdateExponent(int exponentAdd)
    {
        exponent += exponentAdd;
        if (exponent == GameManager.Instance.maxExponent)
        {
            ExplodeNeighbours();
            return;
        }
        var newNumber = (int)Mathf.Pow(2,exponent);
        SetBubbleProperties(newNumber);
    }

    private void ExplodeNeighbours()
    {
        foreach (var neighbour in neighbours)
        {
            neighbour.ExplodeSelf();
        }
        ExplodeSelf();
    }

    public void ExplodeSelf()
    {
        foreach (var neighbour in neighbours)
        {
            neighbour.neighbours.Remove(this);
        }
        Destroy(gameObject,2f);
            
    }
}
