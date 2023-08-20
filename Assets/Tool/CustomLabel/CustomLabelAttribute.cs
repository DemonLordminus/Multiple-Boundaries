using UnityEngine;

/// <summary>
/// 使字段在Inspector中显示自定义的名称。
/// </summary>
public class CustomLabelAttribute : PropertyAttribute
{
    public string name;

    /// <summary>
    /// 使字段在Inspector中显示自定义的名称。
    /// </summary>
    /// <param name="name">自定义名称</param>
    public CustomLabelAttribute(string name)
    {
        this.name = name;
    }
}
