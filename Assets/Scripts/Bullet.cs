using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidbody;

    [SerializeField] private Vector3 force = new Vector3(5, 0, 0);

    [SerializeField] private float delay = 3;
    public int damage = 10;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        myRigidbody.angularVelocity = Vector3.zero;
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.AddForce(force, ForceMode.Impulse);
        Invoke(nameof(WithdrawGameObject), delay);
    }

    private void WithdrawGameObject ()
    {
        gameObject.SetActive(false);
    }
}
