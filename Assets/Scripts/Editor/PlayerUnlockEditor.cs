using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PlayerUnlock))]
public class PlayerUnlockEditor : Editor
{
  public override void OnInspectorGUI()
  {
    PlayerUnlock scriptTarget = target as PlayerUnlock;
    scriptTarget.unlockType = (ProjectionType)EditorGUILayout.EnumPopup(
      new GUIContent("Platform type", "The type of platform that will be effected"),
      scriptTarget.unlockType
      );

    if (scriptTarget.unlockType == ProjectionType.Length)
      scriptTarget.unlockType = ProjectionType.StaticPlatform;

    scriptTarget.destroyOnEnter = EditorGUILayout.Toggle(
      new GUIContent("Destory on enter", "Will destory the cube on a players entry"),
      scriptTarget.destroyOnEnter
      );

    scriptTarget.unlockState = EditorGUILayout.Toggle(
      new GUIContent("State to set", "Sets state of targeting platform, if false with no longer be able to acess that platform"),
      scriptTarget.unlockState
      );

    scriptTarget.changesPrefab = EditorGUILayout.ToggleLeft(
      new GUIContent("Change Prefab", "If selected will spap out prefab"),
      scriptTarget.changesPrefab
      );

    if (scriptTarget.changesPrefab)
    {
      EditorGUI.indentLevel++;
      scriptTarget.newPrefab = (GameObject)EditorGUILayout.ObjectField(
        new GUIContent("New Prefab", "Prefab that will swap with the new platform"),
        scriptTarget.newPrefab,
        typeof(GameObject),
        false
        );
      EditorGUI.indentLevel--;
    }

  }
}
