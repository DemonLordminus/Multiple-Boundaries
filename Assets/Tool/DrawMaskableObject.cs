using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����д����߼���UIԪ�ر�Ե�����߿�
/// ��Scene��ͼ�е�Gizmosȡ����ѡ�ű�������ʱ�ص�
/// ��Ҫ���ڳ����������Ч
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
