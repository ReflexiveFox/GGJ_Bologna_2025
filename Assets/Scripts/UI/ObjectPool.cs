using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();

    [SerializeField] private List<ObjectPoolItem> bulletPrefabs;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < bulletPrefabs.Count; i++)
        {
            for(int j = 0; j < bulletPrefabs[i].amountToPool; j++)
            {
                GameObject obj = Instantiate(bulletPrefabs[i].gameObjectToSpawn);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        GameObject gameObjectToSpawn = null;
        do
        {
            gameObjectToSpawn = pooledObjects[Random.Range(0, pooledObjects.Count-1)];
        }
        while(gameObjectToSpawn.activeInHierarchy);
        return gameObjectToSpawn;
    }
}
