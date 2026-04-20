using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 0.1f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private float verticalRotation = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MouseLook();
    }

    void FixedUpdate()
    {
        Move();
        CheckGrounded();
    }

    void Move()
    {
        // Read WASD keys
        float x = 0f;
        float z = 0f;

        if (Keyboard.current.dKey.isPressed) x = 1f;
        if (Keyboard.current.aKey.isPressed) x = -1f;
        if (Keyboard.current.wKey.isPressed) z = 1f;
        if (Keyboard.current.sKey.isPressed) z = -1f;

        // Move relative to where the player is facing
        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 targetVelocity = move * moveSpeed;

        // Preserve gravity velocity so gravity flipping works correctly
        Vector3 gravityDir = Physics.gravity.normalized;
        float gravityVelocity = Vector3.Dot(rb.linearVelocity, gravityDir);

        rb.linearVelocity = targetVelocity + gravityDir * gravityVelocity;
    }

    void MouseLook()
    {
        // Read mouse delta
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        // Rotate player left/right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera up/down
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
    
    void CheckGrounded()
    {
        Vector3 gravityDir = Physics.gravity.normalized;
        isGrounded = Physics.Raycast(transform.position, gravityDir, 1.1f);
    }
}