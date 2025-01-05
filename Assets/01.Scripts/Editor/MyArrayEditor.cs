using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MyBoolArray))]
public class MyArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Start drawing the label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Find the properties
        SerializedProperty widthProp = property.FindPropertyRelative("Width");
        SerializedProperty heightProp = property.FindPropertyRelative("Height");
        SerializedProperty gridProp = property.FindPropertyRelative("serializedGrid");

        // Adjust position for width and height fields
        Rect sizeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.indentLevel++;

        // Draw Width and Height fields
        EditorGUI.PropertyField(sizeRect, widthProp, new GUIContent("Width"));
        sizeRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(sizeRect, heightProp, new GUIContent("Height"));
        sizeRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        // Validate grid size and update serialized array if necessary
        int width = widthProp.intValue;
        int height = heightProp.intValue;

        if (width < 1) width = 1;
        if (height < 1) height = 1;

        int newSize = width * height;
        if (gridProp.arraySize != newSize)
        {
            gridProp.arraySize = newSize;
        }

        // Draw the grid
        for (int i = 0; i < height; i++)
        {
            Rect rowRect = new Rect(position.x, sizeRect.y + i * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing), position.width, EditorGUIUtility.singleLineHeight);
            for (int j = 0; j < width; j++)
            {
                int index = i * width + j;
                SerializedProperty cellProp = gridProp.GetArrayElementAtIndex(index);

                Rect cellRect = new Rect(rowRect.x + j * 30, rowRect.y, 30, EditorGUIUtility.singleLineHeight);
                cellProp.FindPropertyRelative("value").boolValue = EditorGUI.Toggle(cellRect, cellProp.FindPropertyRelative("value").boolValue);
            }
        }

        EditorGUI.indentLevel--;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty heightProp = property.FindPropertyRelative("Height");
        int height = heightProp.intValue;
        if (height < 1) height = 1;

        return (height + 2) * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
    }
}
