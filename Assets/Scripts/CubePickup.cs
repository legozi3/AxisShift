using UnityEngine;
using UnityEngine.InputSystem;

public class CubePickup : MonoBehaviour
{
    [Header("Settings")]
    public float pickupDistance = 3f; //distance required to pick the cube up
    public float holdDistance = 2f; //how far in front of you the cube hovers
    public Camera playerCamera;

    private GravityCube heldCube = null;
    private Rigidbody heldRb = null;
    public static bool isHoldingCube = false;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame)
        {
            //null indicates empty hand
            if (heldCube == null)
                TryPickup();
            else
                Drop();
        }

        //not null indicates full hand
        if (heldCube != null)
        {
            HoldCube();
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            GravityCube cube = hit.collider.GetComponent<GravityCube>();

            //this means you were not looking at a cube when you clicked
            if (cube == null) return;
            //this means you are not matching the cube's direction
            if (GravityFlip.currentDirection != cube.cubeDirection) return;

            heldCube = cube;
            heldRb = heldCube.GetComponent<Rigidbody>();
            isHoldingCube = true;

            //freezes rotation (does not freeze position)
            heldRb.constraints = RigidbodyConstraints.FreezeRotation;
            heldRb.linearVelocity = Vector3.zero;
            heldRb.angularVelocity = Vector3.zero;
        }
    }

    void HoldCube()
    {
        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;
        
        heldRb.MovePosition(targetPosition);
    
        //kills any velocity the cube may have so it doesn't fall
        heldRb.linearVelocity = Vector3.zero;
        heldRb.angularVelocity = Vector3.zero;
    }

    void Drop()
    {
        isHoldingCube = false;
        //makes the cube behave normally
        heldCube = null;
        heldRb = null;
    }
}