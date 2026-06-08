using UnityEngine;

public class GravityCube : MonoBehaviour
{
    [Header("Settings")]
    public float gravityStrength = 9.81f;
    
    public GravityDirection cubeDirection = GravityDirection.Down;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        bool directionMatches = GravityFlip.currentDirection == cubeDirection;

        if (directionMatches)
        {
            //adds gravity to the cube and makes it so it doesn't fly away if the player collides into it
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            Vector3 gravityForce = GetDirection(cubeDirection) * gravityStrength;
            rb.AddForce(gravityForce, ForceMode.Acceleration);
        }
        else
        {
            //freezes everything so the cube cant be moved.
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
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