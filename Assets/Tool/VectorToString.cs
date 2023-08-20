using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorToString 
{
    public static string ToString(Vector2 dir)
    {
        string direcitonDes = string.Empty;
        if (dir.x > 0)
        {
            direcitonDes += $"右{dir.x}格";
        }
        else if (dir.x < 0)
        {
            direcitonDes += $"左{-dir.x}格";
        }

        if (dir.y > 0)
        {
            direcitonDes += $"上{dir.y}格";
        }
        else if (dir.y < 0)
        {
            direcitonDes += $"下{-dir.y}格";
        }

        return direcitonDes;
    }
}
