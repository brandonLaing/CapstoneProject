using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MiniBlocksSpawner))]
[CanEditMultipleObjects]
public class MiniBlocksSpawnerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    if (GUILayout.Button("SpawnBlocks"))
    {
      MiniBlocksSpawner spawner = target as MiniBlocksSpawner;
      spawner.Activate();
    }
  }
}
