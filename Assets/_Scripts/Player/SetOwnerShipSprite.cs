using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SetOwnerShipSprite : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public override void OnGainedOwnership()
    {
        base.OnGainedOwnership();
        var color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 1f);
    }
    public override void OnLostOwnership()
    {
        base.OnLostOwnership();
        var color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 0.5f);
    }
}
