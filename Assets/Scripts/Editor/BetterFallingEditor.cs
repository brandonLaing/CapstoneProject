using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BetterFalling))]
[CanEditMultipleObjects]
public class BetterFallingEditor : Editor
{
  public override void OnInspectorGUI()
  {
    BetterFalling script = target as BetterFalling;

    script.fallMultiplier = 
      EditorGUILayout.Slider(
        new GUIContent("Fall Multiplier", "This is how much faster the object will fall"),
        script.fallMultiplier, 1, 5);

  }
}
