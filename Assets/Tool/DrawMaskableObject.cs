using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 在所有打开射线检测的UI元素边缘绘制线框
/// 在Scene视图中的Gizmos取消勾选脚本即可暂时关掉
/// 需要挂在场景里才能生效
/// </summary>

public class DrawMaskableObject : MonoBehaviour
{
#if UNITY_EDITOR
    static Vector3[] fourCorners = new Vector3[4];
    private void OnDrawGizmos()
    {
        foreach(MaskableGraphic g in GameObject.FindObjectsOfType<MaskableGraphic>())
        {
            if(g.raycastTarget)
            {
                RectTransform rectTransform = g.transform as RectTransform;
                rectTransform.GetWorldCorners(fourCorners);
                Gizmos.color = Color.blue;
                for (int i = 0; i < 4; i++)
                {
                    Gizmos.DrawLine(fourCorners[i], fourCorners[(i + 1) % 4]);
                }
            }
        }
    }
#endif
}
