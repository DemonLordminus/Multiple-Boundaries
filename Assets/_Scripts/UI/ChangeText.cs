using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public void ChangeTextWithNum(float num)
    {
        text.text = $"{num}";
    }    
}
