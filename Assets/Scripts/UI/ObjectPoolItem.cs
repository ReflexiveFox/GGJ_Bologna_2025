using System;
using UnityEngine;

[Serializable]
public struct ObjectPoolItem
{
    public GameObject gameObjectToSpawn;
    public int amountToPool;
}
