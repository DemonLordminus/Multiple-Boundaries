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
        //֧�ֶ�ѡ
        string[] guids = Selection.assetGUIDs;//��ȡ��ǰѡ�е�asset��GUID
        string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);//ͨ��GUID��ȡ·��
        return assetPath;

    }

}

#endif