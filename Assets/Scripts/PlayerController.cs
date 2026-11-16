using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkingSpeed = 2f;
    public float speedIncrease = 0.1f;
    public float maxSpeed = 10f;
    public float sideSpeed = 5f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Camera playerCamera;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Increase forward speed over time
        walkingSpeed += speedIncrease * Time.deltaTime;
        walkingSpeed = Mathf.Clamp(walkingSpeed, 0f, maxSpeed);

        // Check if the player is on the ground
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // Jump when Space is pressed
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;

        // Left and right movement using A and D
        float horizontalInput = Input.GetAxis("Horizontal");

        // Combine all movement
        Vector3 movement =
            transform.forward * walkingSpeed +
            transform.right * horizontalInput * sideSpeed;

        movement.y = verticalVelocity;

        // Move the Character Controller
        controller.Move(movement * Time.deltaTime);

        // Camera and mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(0, mouseX, 0);
    }
}