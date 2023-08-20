#if UNITY_EDITOR
//SceneNameEditor¿‡
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SceneName))]
public class SceneNameEditor : PropertyDrawer
{
    static GUIContent[] scenes;
    GUIContent[] GetSceneNames()
    {
        GUIContent[] g = new GUIContent[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < g.Length; ++i)
        {
            string[] splitResult = EditorBuildSettings.scenes[i].path.Split('/');
            string nameWithSuffix = splitResult[splitResult.Length - 1];
            g[i] = new GUIContent(nameWithSuffix.Substring(0, nameWithSuffix.Length - ".unity".Length));
        }
        return g;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SceneName sceneName = attribute as SceneName;
        scenes = GetSceneNames();
        string cntString = (string)fieldInfo.GetValue(property.serializedObject.targetObject);
        sceneName.selected = 0;
        sceneName.name = scenes[0].text;
        for (int i = 0; i < scenes.Length; ++i)
        {
            if (scenes[i].text.Equals(cntString))
            {
                sceneName.selected = i;
                sceneName.name = cntString;
                break;
            }
        }
        sceneName.selected = EditorGUI.Popup(position, label, sceneName.selected, scenes);
        sceneName.name = scenes[sceneName.selected].text;
        fieldInfo.SetValue(property.serializedObject.targetObject, sceneName.name);
        ///
        if (GUI.changed)
        {
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
        //
    }
}

#endif