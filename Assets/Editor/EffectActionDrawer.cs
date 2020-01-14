using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EffectAction))]
public class EffectActionDrawer : PropertyDrawer
{
    private const int lineHeight = 16;
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

        int widthEffectRect = 150;
        int widthArgRect = 60;
        int widthArg2Rect = 60;

        if (totalWidth <= 120)
        {
            widthEffectRect = 120;
            widthArgRect = 80;
            widthArg2Rect = 40;
        }
        else
        {
            // fill space
            if (totalWidth < widthEffectRect)
                widthEffectRect = (int)totalWidth;
            float leftHalf = totalWidth*0.6f;
            float rightHalf = totalWidth - leftHalf;
            widthArgRect =  (int) leftHalf;
            widthArg2Rect = (int) rightHalf;
        }

        // float widthleft = totalWidth - widthEffectRect - 5 - widthArg2Rect-5;

        //if (widthleft > widthArgRect) // fill right
        //   widthArgRect = (int) widthleft;

        var effectRect =    new Rect(position.x,    position.y,                            widthEffectRect, lineHeight);
        var argRect =       new Rect(position.x,    position.y + lineHeight,               widthArgRect, lineHeight);
        var arg2Rect =      new Rect(position.x + widthArgRect, position.y + lineHeight,   widthArg2Rect, lineHeight);
       
        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(effectRect, property.FindPropertyRelative("effect"), GUIContent.none);
        EditorGUI.PropertyField(argRect, property.FindPropertyRelative("arg"), GUIContent.none);
        EditorGUI.PropertyField(arg2Rect, property.FindPropertyRelative("arg2"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;
        
        
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return lineHeight * 2+2;
    }

}