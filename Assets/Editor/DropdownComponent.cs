using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractiveTrigger))]
public class YourComponentEditor : Editor
{
    private SerializedProperty dropdownIndexProp;
    private SerializedProperty textFieldProp;
    private SerializedProperty sliderValueProp;

    private string[] dropdownOptions = { "Option 1", "Option 2", "Option 3" };

    private void OnEnable()
    {
        dropdownIndexProp = serializedObject.FindProperty("dropdownIndex");
        textFieldProp = serializedObject.FindProperty("textField");
        sliderValueProp = serializedObject.FindProperty("sliderValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the dropdown selection
        dropdownIndexProp.intValue = EditorGUILayout.Popup("Dropdown", dropdownIndexProp.intValue, dropdownOptions);

        // Check the selected option and show or hide fields accordingly
        switch (dropdownIndexProp.intValue)
        {
            case 0: // Option 1
                textFieldProp.stringValue = EditorGUILayout.TextField("Text Field", textFieldProp.stringValue);
                break;
            case 1: // Option 2
                sliderValueProp.floatValue = EditorGUILayout.Slider("Slider Value", sliderValueProp.floatValue, 0f, 1f);
                break;
            case 2: // Option 3
                // Show or hide any other fields for Option 3
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}