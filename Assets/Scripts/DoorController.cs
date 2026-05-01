using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GravityDirection requiredDirection; //the direction required for the door to work

    public void TryOpen()
    {
        if (GravityFlip.currentDirection == requiredDirection)
        {
            gameObject.SetActive(false); //makes the door disappear
        }
        else
        {
            Debug.Log("Wrong gravity direction, can't open door.");
        }
    }
}