using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class Cannone : MonoBehaviour
{
    [SerializeField] private float delay = 1.5f;

    private float delta = 0;

    [SerializeField] private Transform bulletPosition;

    void Update()
    {
        delta += Time.deltaTime;  //conta il tempo che passa

        if (delta > delay)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null )
        {
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().Shoot(transform.forward);
        }
            
        delta = 0;
    }
}