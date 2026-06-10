using UnityEngine;

public class ColorToX : MonoBehaviour
{
    [Header("Setting")]
    public Color color = Color.red;
    
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.color = color;
    }
}
