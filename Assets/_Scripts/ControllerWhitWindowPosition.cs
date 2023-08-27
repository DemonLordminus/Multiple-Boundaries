using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ControllerWhitWindowPosition : MonoBehaviour
{
    [SerializeField] WindowPositionGetter positionGetter;
    [SerializeField] Transform CameraRoot;
    [SerializeField] float rateBetweenWindowsToGame;//1对应0.00342 100对应0.0343
    
    float rateBetweenWindowsToGameX, rateBetweenWindowsToGameY;
    bool isFocus = false;
    private void Start()
    {
        
        //InvokeRepeating("CameraUpdate",0.5f,0.05f);
    }
    
    public async void CameraUpdate(bool isOnlyOnce = false)
    {
        //Debug.Log("test");

        CameraRoot.transform.position = positionGetter.GetWindowPosition() * rateBetweenWindowsToGame + (Vector2)HostDataManager.Instance.startPoint.position;
        //var pos = positionGetter.GetWindowPosition();
        //CameraRoot.transform.position =  new Vector2(pos.x * rateBetweenWindowsToGameX, pos.y * rateBetweenWindowsToGameY);
        //CameraUpdate();   
        if(!isFocus || isOnlyOnce)
        {
            return;
        }
        await Task.Delay(10);
        CameraUpdate(isOnlyOnce);
    }
    public void ChangeRate(float rate)
    {
        rateBetweenWindowsToGame = rate;
    }
    public void ChangeRate(float rateX,float rateY)
    {
        rateBetweenWindowsToGameX = rateX;
        rateBetweenWindowsToGameY = rateY;
    }
    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
        if(focus)
        {
            CameraUpdate();
        }
    }
}
