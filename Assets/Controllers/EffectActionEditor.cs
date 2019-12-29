using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EffectAction))]
public class EffectActionDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // __prefab__ override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        float totalWidth = position.width;
        int widthEffectRect = 80;
        int widthArgRect = 20;
        float widthleft = totalWidth - widthEffectRect - 5;

        if (widthleft > widthArgRect) // fill right
            widthArgRect = (int) widthleft;

        var effectRect = new Rect(position.x, position.y, widthEffectRect, position.height);
        var argRect = new Rect(position.x + widthEffectRect +5, position.y, widthArgRect, position.height);
        
        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(effectRect, property.FindPropertyRelative("effect"), GUIContent.none);
        EditorGUI.PropertyField(argRect, property.FindPropertyRelative("arg"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;
        
        EditorGUI.EndProperty();
    }
}