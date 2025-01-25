using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class Cannone : MonoBehaviour
{
    [SerializeField] private float delay = 1.5f;

    private float delta = 0;

    [SerializeField] private Bullet bulletTemplate;

    [SerializeField] private Transform bulletPosition;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        delta += Time.deltaTime;  //conta il tempo che passa
        
        if (delta > delay)
        {
            GameObject bullet = ObjectPool.instance.GetPooledObject();

            if (bullet != null )
            {
                bullet.transform.position = bulletPosition.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
            }
            
            delta = 0;
        }
    }
}