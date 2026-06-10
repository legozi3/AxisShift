using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Setting")]
    public DoorController connectedDoor;
    
    private bool isActivated = false;

    //turns the door off
    void OnTriggerEnter(Collider col)
    {
        //gets the component of the collider object
        if (col.gameObject.GetComponent<GravityCube>() != null)
        {
            isActivated = true;
            connectedDoor.TryOpen(this);
        }
    }

    //turns the door back on
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<GravityCube>() != null)
        {
            isActivated = false;
            connectedDoor.Reappear(this);
        }
    }

    public bool IsActivated() => isActivated;
}