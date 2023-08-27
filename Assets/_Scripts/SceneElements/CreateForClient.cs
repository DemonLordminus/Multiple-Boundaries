using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CreateForClient : MonoBehaviour
{
    [SerializeField] private List<ulong> clientsID;
    //[SerializeField] private bool isJustSetFalse;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [EnumToggleButtons]
    [SerializeField] CreateOption option;
    [ShowIf("option", CreateOption.SetSprite)]
    [SerializeField] private Sprite newSprite;
    [ShowIf("option", CreateOption.SetColor)]
    [SerializeField] private Color newColor;
    [SerializeField] private bool isOnlyFor;
    private void OnEnable()
    {
        NetworkManager.Singleton.SceneManager.OnSceneEvent += OnSceneEvent;
    }
    private void OnDisable()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.SceneManager.OnSceneEvent -= OnSceneEvent; 
        }
    }
    private void OnSceneEvent(SceneEvent sceneEvent)
    {
        switch (sceneEvent.SceneEventType)
        {
            case SceneEventType.Load:
                break;
            case SceneEventType.Unload:
                break;
            case SceneEventType.Synchronize:
                break;
            case SceneEventType.ReSynchronize:
                break;
            case SceneEventType.LoadEventCompleted:
                break;
            case SceneEventType.UnloadEventCompleted:
                break;
            case SceneEventType.LoadComplete:
                if(spriteRenderer==null)
                {
                    spriteRenderer = GetComponent<SpriteRenderer>();
                }
                if (clientsID.Contains(NetworkManager.Singleton.LocalClientId) == isOnlyFor)
                {
                    HandleOnSceneLoad();
                }
                break;
            case SceneEventType.UnloadComplete:
                break;
            case SceneEventType.SynchronizeComplete:
                break;
            case SceneEventType.ActiveSceneChanged:
                break;
            case SceneEventType.ObjectSceneChanged:
                break;
        }
    }
    private void DestroyForID()
    {    
        Destroy(gameObject);
    }
    private void SetFalseForNonID()
    {
        
         gameObject.SetActive(false);
        
    }
    private void SetSprite()
    {
        
         spriteRenderer.sprite = newSprite;
        
    }
    private void SetColor()
    {
       
        spriteRenderer.color = newColor;
        
    }
    private void HandleOnSceneLoad()
    {
        switch (option)
        {
            case CreateOption.DestroyThis:
                DestroyForID();
                break;
            case CreateOption.JustSetFalse:
                SetFalseForNonID();
                break;
            case CreateOption.SetColor:
                SetColor();
                break;
            case CreateOption.SetSprite:
                SetSprite();
                break;
            case CreateOption.empty:
                break;
        }
    }

}
public enum CreateOption
{ 
    empty,
    DestroyThis,
    JustSetFalse,
    SetColor,
    SetSprite,
}