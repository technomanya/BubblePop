using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BubbleData))]
public class BubbleDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BubbleData bubbleDataScript = (BubbleData)target;

        bubbleDataScript.number = EditorGUILayout.IntField("Bubble Number", bubbleDataScript.number);
        
        if (GUILayout.Button("Set Bubble Number"))
        {
            bubbleDataScript.SetBubbleProperties(bubbleDataScript.number);
        }
    }
}
