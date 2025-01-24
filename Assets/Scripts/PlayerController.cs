using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField, Tooltip("Player movement speed.")]
    private float moveSpeed = 5f;

    [SerializeField, Tooltip("Player rotation speed.")]
    private float rotationSpeed = 10f;

    [Header("Camera")]
    [SerializeField, Tooltip("Reference to the Cinemachine FreeLook Camera.")]
    private CinemachineCamera freeLookCamera;

    private CharacterController characterController;
    private Vector2 inputMove;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (freeLookCamera == null)
        {
            Debug.LogWarning("No Cinemachine FreeLook Camera assigned.");
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Convert input into a world space direction
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Ignore vertical camera tilt
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * inputMove.y + right * inputMove.x).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.LookRotation(forward);
        }

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue inputValue)
    {
        Debug.Log("Move input detected.");
        inputMove = inputValue.Get<Vector2>();
    }
}
