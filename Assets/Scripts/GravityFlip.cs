using UnityEngine;
using UnityEngine.InputSystem;

public class GravityFlip : MonoBehaviour
{
    [Header("Settings")]
    public float raycastDistance = 3f;      //how close you need to be to a wall
    public float transitionSpeed = 5f;      //how fast the transition is
    public float gravityStrength = 9.81f;   //the strength of the gravity
    public Camera playerCamera;
    
    public static GravityDirection currentDirection = GravityDirection.Down;

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
        //cannot change gravity when holding a cube
        if (CubePickup.isHoldingCube) return;
        
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
            
            currentDirection = GetDirectionFromVector(newGravityDir);

            targetRotation = Quaternion.FromToRotation(transform.up, wallNormal) * transform.rotation;
            isTransitioning = true;
        }
    }
    
    GravityDirection GetDirectionFromVector(Vector3 dir)
    {
        //rounds the vector so it doesn't cause floating point precision problems
        Vector3 rounded = new Vector3(
            Mathf.Round(dir.x),
            Mathf.Round(dir.y),
            Mathf.Round(dir.z)
        );

        switch (rounded)
        {
            case var d when d == Vector3.down:    return GravityDirection.Down;
            case var d when d == Vector3.up:      return GravityDirection.Up;
            case var d when d == Vector3.left:    return GravityDirection.Left;
            case var d when d == Vector3.right:   return GravityDirection.Right;
            case var d when d == Vector3.forward: return GravityDirection.Forward;
            case var d when d == Vector3.back:    return GravityDirection.Back;
            default:                                      return GravityDirection.Down;
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