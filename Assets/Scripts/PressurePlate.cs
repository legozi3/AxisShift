using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public DoorController connectedDoor;
    
    private bool isActivated = false;

    void OnCollisionEnter(Collision col)
    {
        //gets the component of the collider object
        if (col.gameObject.GetComponent<GravityCube>() != null)
        {
            isActivated = true;
            connectedDoor.TryOpen(this);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.GetComponent<GravityCube>() != null)
        {
            isActivated = false;
            connectedDoor.Reappear(this);
        }
    }

    public bool IsActivated() => isActivated;
}