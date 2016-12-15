using System;
using UnityEngine;

public static class Utils
{
    private static System.Random Random = new System.Random();

    public static Vector2 GetRandom(this Vector2[] list)
    {
        return list[Random.Next(0, list.Length - 1)];
    }

    public static TGameObject Create<TGameObject>(this GameObject gameObject, Vector2 position, Transform parent)
    {
        return gameObject.Create(position, parent).GetComponent<TGameObject>();
    }

    public static GameObject Create(this GameObject @object, Vector2 position, Transform parent)
    {
        return GameObject.Instantiate<GameObject>(@object, new Vector3(position.x, 0, position.y), Quaternion.Euler(90, 0, 0), parent);
    }
}
