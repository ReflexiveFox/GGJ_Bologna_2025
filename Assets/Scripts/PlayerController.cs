using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField, Tooltip("Player movement speed.")]
    private float moveSpeed = 5f;

    [SerializeField, Tooltip("Player jump height.")]
    private float jumpHeight = 2f;

    [SerializeField, Tooltip("Gravity value.")]
    private float gravity = -9.81f;

    [Header("Dash Settings")]
    [SerializeField, Tooltip("Dash speed.")]
    private float dashSpeed = 20f;

    [SerializeField, Tooltip("Dash duration.")]
    private float dashDuration = 0.2f;

    [SerializeField, Tooltip("Dash cooldown.")]
    private float dashCooldown = 1f;

    [Header("Camera")]
    [SerializeField, Tooltip("Reference to the Cinemachine FreeLook Camera.")]
    private CinemachineCamera freeLookCamera;
    private Camera cam;

    private CharacterController characterController;
    private Rigidbody playerRb;
    private Vector2 inputMove;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isDashing;
    private float dashTime;
    private float dashCooldownTime;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerRb = GetComponent<Rigidbody>();
        if (freeLookCamera == null)
        {
            Debug.LogWarning("No Cinemachine FreeLook Camera assigned.");
        }
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        playerRb.maxAngularVelocity = 0;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to ensure the player stays grounded
        }

        // Convert input into a world space direction
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        // Ignore vertical camera tilt
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * inputMove.y + right * inputMove.x).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(forward);
        }

        if (isDashing)
        {
            characterController.Move(dashSpeed * Time.deltaTime * moveDirection);
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
                dashCooldownTime = dashCooldown;
            }
        }
        else
        {
            characterController.Move(moveSpeed * Time.deltaTime * moveDirection);

            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

            if (dashCooldownTime > 0)
            {
                dashCooldownTime -= Time.deltaTime;
            }
        }
    }

    public void OnMove(InputValue inputValue)
    {
        inputMove = inputValue.Get<Vector2>();
    }

    public void OnJump(InputValue inputValue)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnSprint(InputValue inputValue)
    {
        if (dashCooldownTime <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
        }
    }
}
