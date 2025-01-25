using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private int amountToPool = 8;

    [SerializeField] private List<GameObject> bulletPrefabs;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            CreateAndAddRandomBulletObject();
        }
    }

    private GameObject CreateAndAddRandomBulletObject()
    {
        GameObject obj = Instantiate(bulletPrefabs[Random.Range(0, bulletPrefabs.Count - 1)]);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        return CreateAndAddRandomBulletObject();
    }
}
