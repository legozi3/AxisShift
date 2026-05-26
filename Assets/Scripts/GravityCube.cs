using System;
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
        rb.useGravity = false;
        TryMatchGravity();
    }

    void Update()
    {
        HandleInput();
        
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
        if (GravityFlip.currentDirection == cubeDirection)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce((GetDirection(cubeDirection) * gravityStrength), ForceMode.Acceleration);
            Debug.Log("gravity strength: " + GetDirection(cubeDirection) * gravityStrength);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
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