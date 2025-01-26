using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody myRigidbody;

    [SerializeField] private float force = 50;

    [SerializeField] private float delay = 3;
    public int damage = 10;

    private void Awake()
    {
        myRigidbody = GetComponentInChildren<Rigidbody>();
    }

    public void Shoot (Vector3 shootingDirection)
    {
        myRigidbody.angularVelocity = Vector3.zero;
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.AddForce(shootingDirection * force, ForceMode.Impulse);
        Invoke(nameof(WithdrawGameObject), delay);
    }


    private void WithdrawGameObject ()
    {
        gameObject.SetActive(false);
    }
}
