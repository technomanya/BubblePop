using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelMaker))]
public class LevelMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelMaker gmScript = (LevelMaker)target;

        gmScript.currBubbleNumber = EditorGUILayout.IntField("Bubble Number", gmScript.currBubbleNumber);
        
        if(GUILayout.Button("Place Bubble"))
        {
            gmScript.PlaceBubble();
        }

        gmScript.bubbleAmount = EditorGUILayout.IntField("Bubble Amount", gmScript.bubbleAmount);
        
        if(GUILayout.Button("Place Multiple Bubbles"))
        {
            gmScript.PlaceMultiBubbles();
        }
        
        gmScript.minExponent = EditorGUILayout.IntField("Min exponent of two", gmScript.minExponent);
        gmScript.maxExponent = EditorGUILayout.IntField("Max exponent of two", gmScript.maxExponent);
        if(GUILayout.Button("Place Multiple Random Bubbles"))
        {
            gmScript.PlaceRandomMultiBubbles();
        }
        
        if(GUILayout.Button("Reset Level Screen"))
        {
            gmScript.ResetLevelScreen();
        }
    }
}
