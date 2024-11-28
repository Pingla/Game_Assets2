using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;          // Walking speed
    public float sprintSpeed = 10f;      // Sprint speed
    public float mouseSensitivity = 2f;  // Mouse sensitivity for looking around
    public float jumpHeight = 2f;        // Jump Height
    public float gravity = -9.81f;       // Strength of gravity
    public Transform cameraTransform;    // Reference to the camera

    private CharacterController controller;
    private Vector3 movement;
    private Vector3 velocity;
    private float verticalRotation = 0f; // Tracks vertical camera rotation

    void Start()
    {
        
        controller = GetComponent<CharacterController>();

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        
        bool isGrounded = controller.isGrounded;

        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Determine if the player is sprinting
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); // Left/Right or A/D
        float moveZ = Input.GetAxis("Vertical");   // Forward/Backward or W/S

        
        movement = transform.right * moveX + transform.forward * moveZ;

        
        controller.Move(movement * currentSpeed * Time.deltaTime);

        
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

