using UnityEngine;

/// <summary>
/// ʹ�ֶ���Inspector����ʾ�Զ�������ơ�
/// </summary>
public class CustomLabelAttribute : PropertyAttribute
{
    public string name;

    /// <summary>
    /// ʹ�ֶ���Inspector����ʾ�Զ�������ơ�
    /// </summary>
    /// <param name="name">�Զ�������</param>
    public CustomLabelAttribute(string name)
    {
        this.name = name;
    }
}
