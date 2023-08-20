using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRect : MonoBehaviour
{
    [SerializeField] private Vector2 rect;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, rect);
    }
}
