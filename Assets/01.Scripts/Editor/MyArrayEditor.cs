using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MyBoolArray))]
public class MyArrayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MyBoolArray script = (MyBoolArray)target;

        EditorGUI.BeginChangeCheck();

        int newRows = EditorGUILayout.IntField("Rows", script.rows);
        int newColumns = EditorGUILayout.IntField("Columns", script.columns);

        // 크기 변경 시 Undo 처리
        if (newRows != script.rows || newColumns != script.columns)
        {
            Undo.RecordObject(script, "Change Array Size");
            script.rows = newRows;
            script.columns = newColumns;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Grid Values:", EditorStyles.boldLabel);

        for (int i = 0; i < script.rows; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < script.columns; j++)
            {
                bool currentValue = script.GetValue(i, j);
                bool newValue = EditorGUILayout.Toggle(currentValue, GUILayout.Width(30));

                // 값 변경 시 Undo 처리
                if (currentValue != newValue)
                {
                    Undo.RecordObject(script, "Toggle Grid Value");
                    script.SetValue(i, j, newValue);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        if (EditorGUI.EndChangeCheck())
        {
            // 변경 사항이 있을 때만 'SetDirty' 호출
            EditorUtility.SetDirty(script);
        }
    }
}