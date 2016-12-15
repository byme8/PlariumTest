using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Utils
{
    private static System.Random Random = new System.Random();

    public static Vector2 GetRandom(this Vector2[] list)
    {
        return list[Random.Next(0, list.Length - 1)];
    }
}
