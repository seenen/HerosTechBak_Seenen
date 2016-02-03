using UnityEngine;
using System.Collections;

public static class ObjectPoolExtensions
{
    public static void CreatePool<T>(this T prefab) where T : Component
    {
        ObjectPool.CreatePool(prefab);
    }

    public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return ObjectPool.Spawn(prefab, position, rotation);
    }
    public static T Spawn<T>(this T prefab, Vector3 position) where T : Component
    {
        return ObjectPool.Spawn(prefab, position, Quaternion.identity);
    }
    public static T Spawn<T>(this T prefab) where T : Component
    {
        return ObjectPool.Spawn(prefab, Vector3.zero, Quaternion.identity);
    }

    public static void Recycle<T>(this T obj) where T : Component
    {
        ObjectPool.Recycle(obj);
    }

    public static int Count<T>(T prefab) where T : Component
    {
        return ObjectPool.Count(prefab);
    }
}
