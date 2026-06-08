using UnityEngine;
using UnityEngine.InputSystem;

public class CubePickup : MonoBehaviour
{
    [Header("Settings")]
    public float pickupDistance = 3f;
    public float holdDistance = 2f;
    public Camera playerCamera;

    private GravityCube heldCube = null;
    private Rigidbody heldRb = null;
    public static bool isHoldingCube = false;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldCube == null)
                TryPickup();
            else
                Drop();
        }

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

            if (cube == null) return;
            if (GravityFlip.currentDirection != cube.cubeDirection) return;

            heldCube = cube;
            heldRb = heldCube.GetComponent<Rigidbody>();
            isHoldingCube = true;

            //freezes rotation, does not freeze position
            heldRb.constraints = RigidbodyConstraints.FreezeRotation;
            heldRb.linearVelocity = Vector3.zero;
            heldRb.angularVelocity = Vector3.zero;
        }
    }

    void HoldCube()
    {
        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;
    
        // MovePosition respects physics/collisions unlike setting transform.position directly
        heldRb.MovePosition(targetPosition);
    
        // Kill any velocity each frame so it doesn't drift or fall
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