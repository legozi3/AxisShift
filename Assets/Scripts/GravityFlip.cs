using UnityEngine;
using UnityEngine.InputSystem;

public class GravityFlip : MonoBehaviour
{
    [Header("Settings")]
    public float raycastDistance = 3f;      //how close you need to be to a wall
    public float transitionSpeed = 5f;      //how fast the transition is
    public float gravityStrength = 9.81f;   //the strength of the gravity
    public Camera playerCamera;

    private Quaternion targetRotation;
    private bool isTransitioning = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    void Update()
    {
        HandleInput();

        if (isTransitioning)
        {
            SmoothTransition();
        }
    }

    void HandleInput()
    {
        //if an RMB input was detected, try to flip the gravity
        if (Mouse.current.rightButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryFlipGravity();
        }
    }

    void TryFlipGravity()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            Vector3 wallNormal = hit.normal;
            
            //the floor you're standing on
            Vector3 currentFloor = -Physics.gravity.normalized;
            float sameAsCurrentFloor = Vector3.Dot(wallNormal, currentFloor);

            //stops you from flipping to the floor you're standing on
            if (sameAsCurrentFloor > 0.7f)
            {
                Debug.Log("Already standing on this surface.");
                return;
            }

            Vector3 newGravityDir = -wallNormal;
            Physics.gravity = newGravityDir * gravityStrength;

            targetRotation = Quaternion.FromToRotation(transform.up, wallNormal) * transform.rotation;
            isTransitioning = true;
        }
    }

    void SmoothTransition()
    {
        GetComponent<PlayerMovement>().enabled = false;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * transitionSpeed
        );

        //stop transitioning once its close enough to the target
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
        {
            transform.rotation = targetRotation;
            isTransitioning = false;
            GetComponent<PlayerMovement>().enabled = true;
        }
    }
}