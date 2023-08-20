using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public class CreateReadMe : MonoBehaviour
{
    [MenuItem("Assets/Create/Readme", false, 110)]
    static void CreateTxt()
    {
        string path = LogPath() + "\\Readme.txt";
        if (!File.Exists(path))
        {
            FileInfo readme = new FileInfo(path);
            StreamWriter sw = readme.CreateText();
            sw.Close();
            sw.Dispose();
            AssetDatabase.Refresh();
        }


    }
    static string LogPath()
    {
        //支持多选
        string[] guids = Selection.assetGUIDs;//获取当前选中的asset的GUID
        string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);//通过GUID获取路径
        return assetPath;

    }

}

#endif