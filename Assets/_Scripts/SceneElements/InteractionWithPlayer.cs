using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionWithPlayer : MonoBehaviour
{
    [SerializeField] GameObject keyTip;
    [SerializeField] LayerMask playerLayer;
    public UnityEvent<GameObject> OnInteract;
    [SerializeField] GameObject interactObject;
    bool isCanInteract = false;
    private void Start()
    {
        keyTip.SetActive(false);
    }
    private void OnEnable()
    {
        InputManager.OnInteract += Interact;
    }
    private void OnDisable()
    {
        InputManager.OnInteract -= Interact;
    }
    private void Interact()
    {
        if(isCanInteract)
        {
            OnInteract?.Invoke(interactObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) > 0 )
        {
            interactObject = collision.gameObject;
            isCanInteract = true;
            if(keyTip!=null)
            {
                keyTip.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) > 0)
        {
            interactObject = collision.gameObject;
            isCanInteract = false;
            if (keyTip != null)
            {
                keyTip.SetActive(false);
            }
        }
    }
}
