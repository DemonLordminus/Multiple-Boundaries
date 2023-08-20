using UnityEditor;
using UnityEngine;

/// <summary>
/// ����Դ��� `CustomLabelAttribute` ���Ե��ֶε�������ݵĻ�����Ϊ��
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