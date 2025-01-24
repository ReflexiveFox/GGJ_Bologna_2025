using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class Cannone : MonoBehaviour
{
    private float delay = 1.5f;

    private float delta = 0;

    [SerializeField] private Bullet bulletTemplate;

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
            Bullet bullet = Instantiate(bulletTemplate, transform.position, transform.rotation);
            bullet.gameObject.SetActive(true);
            delta = 0;
        }
    }
}