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

    // Show fall multiplier
    script.fallMultiplier = 
      EditorGUILayout.Slider(
        new GUIContent("Fall Multiplier", "This is how much faster the object will fall"),
        script.fallMultiplier, 1, 5);

    // If the game is playing only show is player and if it isnt allow them to set it
    // this is for the input manager stuff
    if (Application.isPlaying)
      EditorGUILayout.Toggle(
      new GUIContent("Is Player", "Tells whether or not the object is the player. Can only be set during edit time"),
      script.isPlayer);
    else
      script.isPlayer = EditorGUILayout.Toggle(
      new GUIContent("Is Player", "Tells whether or not the object is the player. Can only be set during edit time"), 
      script.isPlayer);

    // show is player setting if the objec is the player
    if (script.isPlayer)
    {
      // increase the indent level
      EditorGUI.indentLevel++;

      // show the low jump setter
      script.lowJumpMultiplier = EditorGUILayout.Slider(
        new GUIContent("Low Jump Multiplier", "Fall multiplier while player is pessing jump"),
        script.lowJumpMultiplier, 1, 5);

      EditorGUILayout.Toggle(
        new GUIContent("Is Jumping", "This shows if the player is currently jumping"),
        script.IsJumping);
      EditorGUI.indentLevel--;

      if (script.lowJumpMultiplier > script.fallMultiplier)
        script.lowJumpMultiplier = script.fallMultiplier;
    }
  }
}
