#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadExcel))]
public class LoadExcelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LoadExcel myScript = (LoadExcel)target;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Load Word Data"))
        {
            myScript.LoadWorddData();
        }

    }
}
#endif
