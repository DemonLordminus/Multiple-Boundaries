using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PositionChangeButton : MonoBehaviour
{
    [SerializeField] Button changeButton;
    [SerializeField] TMP_InputField x, y;
    [SerializeField] WindowPositionGetter positionGetter;
    [SerializeField] TextMeshProUGUI showNowTrans;
    [SerializeField] Transform cameraRoot;
    private void Start()
    {
        changeButton.onClick.AddListener(() =>
        {
            positionGetter.offsetX = int.Parse(x.text); 
            positionGetter.offsetY = int.Parse(y.text);
            positionGetter.SetWindowsPositionToCenter();
            showNowTrans.text = cameraRoot.position.ToString();
        });
    }
}
