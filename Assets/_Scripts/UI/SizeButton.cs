using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeButton : MonoBehaviour
{

    [SerializeField] private TMP_InputField textX,textY;
    [SerializeField] private ControllerWhitWindowPosition contoller;
    [SerializeField] Button button;
    private void Start()
    {
        button.onClick.AddListener(() => { contoller.ChangeRate(float.Parse(textX.text), float.Parse(textY.text)); });
        //slider.onValueChanged.AddListener((t) => { text.text = $"{t}"; contoller.ChangeRate(t); });

    }
}
