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
            direcitonDes += $"��{dir.x}��";
        }
        else if (dir.x < 0)
        {
            direcitonDes += $"��{-dir.x}��";
        }

        if (dir.y > 0)
        {
            direcitonDes += $"��{dir.y}��";
        }
        else if (dir.y < 0)
        {
            direcitonDes += $"��{-dir.y}��";
        }

        return direcitonDes;
    }
}
