using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GravityDirection requiredDirection; //the direction required for the door to work

    [Header("Pressure Plates")]
    public PressurePlate plateOne; //leave both empty if connected to a button
    public PressurePlate plateTwo; //leave empty if only one plate
    
    public void TryOpen(PressurePlate plate)
    {
        //return if gravity directions don't match
        if (GravityFlip.currentDirection != requiredDirection) return;
        
        //checks for plateTwo if applicable
        if (plateTwo != null)
        {
            if (plateOne.IsActivated() && plateTwo.IsActivated())
                gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
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
    public void Reappear(PressurePlate plate)
    {
        gameObject.SetActive(true);
    }

}