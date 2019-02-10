using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformSpeed))]
[CanEditMultipleObjects]
public class PlatformSpeedEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    if (GUILayout.Button("Test"))
    {
      PlatformSpeed thing = target as PlatformSpeed;
      thing.TestOnObjects();
    }
  }
}
