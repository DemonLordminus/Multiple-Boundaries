using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerSetStartPos : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await Task.Yield();
        SetToStartPos();
    }
    public void SetToStartPos()
    {
        transform.position = PlayerStartPoint.instance.transform.position;
    }
    private void Update()
    {
        if (transform.position.y < -70)
        {
            SetToStartPos();
        }    
    }
}
