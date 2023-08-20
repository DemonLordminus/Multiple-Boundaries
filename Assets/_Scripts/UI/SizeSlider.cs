using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeSlider : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ControllerWhitWindowPosition contoller;
    [SerializeField] Slider slider;
    private void Start()
    {
        //slider.onValueChanged.AddListener((t) => { text.text = $"{t}"; contoller.ChangeRate(t); });
    }
}
