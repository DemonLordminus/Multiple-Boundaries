using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelAttribute : PropertyAttribute
{
    public string label;
    public LabelAttribute(string label)
    {
        this.label = label;
    }
}

