using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Method)]
public class EditorButtonAttribute : PropertyAttribute
{
    public string customName=null   ;
    public EditorButtonAttribute()
    {
        customName = null;
    }
    public EditorButtonAttribute(string _name)
    {
        customName = _name;
    }
}