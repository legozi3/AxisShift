using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public DoorController connectedDoor;
    
    private bool isActivated = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<GravityCube>() != null)
        {
            isActivated = true;
            connectedDoor.TryOpen(this);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<GravityCube>() != null)
        {
            isActivated = false;
            connectedDoor.ResetPlate(this);
        }
    }

    public bool IsActivated() => isActivated;
}