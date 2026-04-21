using UnityEngine;

public class ColorToRed : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    
    void FixedUpdate()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
}
