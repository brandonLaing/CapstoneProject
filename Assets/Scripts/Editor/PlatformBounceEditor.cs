using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformBounce))]
[CanEditMultipleObjects]
public class PlatformBounceEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    if (GUILayout.Button("Test"))
    {
      PlatformBounce thing = target as PlatformBounce;
      thing.TestOnObjects();
    }
  }
}
