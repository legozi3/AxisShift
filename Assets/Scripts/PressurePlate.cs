using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Setting")]
    public DoorController connectedDoor;
    
    private bool isActivated = false;

    void OnTriggerEnter(Collider col)
    {
        //gets the component of the collider object
        if (col.gameObject.GetComponent<GravityCube>() != null)
        {
            isActivated = true;
            connectedDoor.TryOpen(this);
        }
    }

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