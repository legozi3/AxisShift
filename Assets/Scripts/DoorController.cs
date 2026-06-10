using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GravityDirection requiredDirection; //the direction required for the door to work

    [Header("Pressure Plates")]
    public PressurePlate plateOne;
    public PressurePlate plateTwo;
    
    public void TryOpen(PressurePlate callingPlate)
    {
        if (GravityFlip.currentDirection == requiredDirection)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong gravity direction, can't open door.");
        }
    }
    
    public void ResetPlate(PressurePlate callingPlate)
    {
        gameObject.SetActive(true);
    }
    
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