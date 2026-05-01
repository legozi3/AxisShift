using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    public DoorController connectedDoor;  //where the door goes
    public float interactDistance = 3f;
    public Camera playerCamera;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            //makes sure the button was actually clicked
            if (hit.collider.gameObject == gameObject)
            {
                connectedDoor.TryOpen();
            }
        }
    }
}