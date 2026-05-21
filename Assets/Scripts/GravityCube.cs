using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class GravityCube : MonoBehaviour
{
    [Header("Settings")]
    public float gravityStrength = 9.81f;   //the strength of the gravity
    //public GravityDirection direction = GravityDirection.Down;

    private GravityDirection cubeDirection = GravityDirection.Down;
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TryMatchGravity();
    }

    void Update()
    {
        HandleInput();
        //rb.velocity = GetDirection(cubeDirection) * gravityStrength;
        
    }

    void HandleInput()
    {
        //if an RMB input was detected, try to match the gravity
        if (Mouse.current.rightButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryMatchGravity();
        }
    }

    void TryMatchGravity()
    {

        if (GravityFlip.currentDirection == GravityDirection.Down)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
    
    Vector3 GetDirection(GravityDirection dir)
    {
        switch (dir)
        {
            case GravityDirection.Down:    return new Vector3(0, -1, 0);
            case GravityDirection.Up:      return new Vector3(0, 1, 0);
            case GravityDirection.Left:    return new Vector3(-1, 0, 0);
            case GravityDirection.Right:   return new Vector3(1, 0, 0);
            case GravityDirection.Forward: return new Vector3(0, 0, 1);
            case GravityDirection.Back:    return new Vector3(0, 0, -1);
            default:                       return new Vector3(0, -1, 0);
        }
    }
}