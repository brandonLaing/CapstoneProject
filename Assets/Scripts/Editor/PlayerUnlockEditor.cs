using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PlayerUnlock))]
public class PlayerUnlockEditor : Editor
{
  private readonly bool editorFuckingUp = true;

  public override void OnInspectorGUI()
  {
    if (editorFuckingUp)
    {
      DrawDefaultInspector();
      return;
    }

    PlayerUnlock _target = target as PlayerUnlock;

    BuildBaseElements(_target);
    if (_target.unlockState)
      BuildSwap(_target);
  }

  private void BuildSwap(PlayerUnlock target)
  {
    EditorGUI.indentLevel++;
    target.changesPrefab = EditorGUILayout.ToggleLeft(
      new GUIContent("Changes prefab"),
      target.changesPrefab
      );
    if (target.changesPrefab)
    {
      EditorGUI.indentLevel++;
      target.newPrefab = (GameObject)EditorGUILayout.ObjectField(
        new GUIContent("New Prefab", "This prefab swaps out for your old one"),
        target.newPrefab,
        typeof(GameObject),
        false
        );
      EditorGUI.indentLevel--;
    }
    EditorGUI.indentLevel--;
  }

  private void BuildBaseElements(PlayerUnlock target)
  {
    target.unlockType = (ProjectionType)EditorGUILayout.EnumPopup(
      new GUIContent("Platform to effect"),
      target.unlockType
      );

    target.destroyOnEnter = EditorGUILayout.ToggleLeft(
      new GUIContent("Destroy after interaction"),
      target.destroyOnEnter
      );

    target.unlockState = EditorGUILayout.ToggleLeft(
      new GUIContent("Unlock/Lock", "True unlocks False locks"),
      target.unlockState
  );
  }
}
