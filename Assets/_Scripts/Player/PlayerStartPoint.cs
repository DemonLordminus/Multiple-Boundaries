using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    public static PlayerStartPoint instance;
    private void Start()
    {
        instance = this;
    }
}
