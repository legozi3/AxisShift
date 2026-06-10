using UnityEngine;

public class GravityColorManager : MonoBehaviour
{
    [Header("Settings")]
    public GravityDirection desiredDirection;
    public Color activeColor = Color.red;

    private static readonly Color defaultColor = new Color(0.5f, 0.5f, 0.5f);
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.color = (GravityFlip.currentDirection == desiredDirection) ? activeColor : defaultColor;
    }
}