using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ForestGenerator))]
public class ForestGeneratorGUI : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        ForestGenerator forest = (ForestGenerator)target;
        if(GUILayout.Button("Generate Forest")){
            forest.GenerateNewForest();
        }

        if(GUILayout.Button("Delete Forest")){
            forest.DeleteCurrentForest();
        }

        if(GUILayout.Button("Cement Forest")){
            forest.CementCurrentForest();
        }

    }
}
