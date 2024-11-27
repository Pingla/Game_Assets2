using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;          // Normal walking speed
    public float sprintSpeed = 10f;      // Speed while sprinting
    public float mouseSensitivity = 2f;  // Mouse sensitivity for looking around
    public float jumpHeight = 2f;        // Height of the jump
    public float gravity = -9.81f;       // Strength of gravity
    public Transform cameraTransform;    // Reference to the camera

    private CharacterController controller;
    private Vector3 movement;
    private Vector3 velocity;
    private float verticalRotation = 0f; // Tracks vertical camera rotation

    void Start()
    {
        // Get the CharacterController component
        controller = GetComponent<CharacterController>();

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Automatically find the camera if not assigned
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Check if the player is grounded
        bool isGrounded = controller.isGrounded;

        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Determine if the player is sprinting
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); // Left/Right or A/D
        float moveZ = Input.GetAxis("Vertical");   // Forward/Backward or W/S

        // Calculate movement direction relative to the player
        movement = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        controller.Move(movement * currentSpeed * Time.deltaTime);

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply vertical velocity
        controller.Move(velocity * Time.deltaTime);

        // Handle mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically with clamping
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
    }
}

