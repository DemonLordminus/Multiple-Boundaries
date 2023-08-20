using UnityEditor;
using UnityEngine;

/// <summary>
/// 定义对带有 `CustomLabelAttribute` 特性的字段的面板内容的绘制行为。
/// </summary>
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(CustomLabelAttribute))]
public class CustomLabelDrawer : PropertyDrawer
{
    private GUIContent _label = null;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_label == null)
        {
            string name = (attribute as CustomLabelAttribute).name;
            _label = new GUIContent(name);
        }

        EditorGUI.PropertyField(position, property, _label);
    }
}

#endif