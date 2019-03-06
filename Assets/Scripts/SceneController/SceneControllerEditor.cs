using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneController))]
public class SceneControllerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    SceneController self = target as SceneController;

    if (GUILayout.Button("Load Next Scene"))
      self.LoadNextScene();

    if (GUILayout.Button("Move Player Over"))
      self.MovePlayerOver();

    if (GUILayout.Button("Close previous Scene"))
      self.ClosePreviousScene();

    if (GUILayout.Button("Unload previousScene"))
      self.UnloadPreviousScene();
  }
}
