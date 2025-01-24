using UnityEngine;
using static UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidbody;

    [SerializeField] private float speed = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody.AddForce(Vector3.right * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
