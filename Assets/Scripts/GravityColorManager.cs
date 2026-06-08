using UnityEngine;

public class GravityColorManager : MonoBehaviour
{
    public GravityDirection facingDirection;
    public Color activeColor = Color.red;

    private static readonly Color greyColor = new Color(0.5f, 0.5f, 0.5f);
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.color = (GravityFlip.currentDirection == facingDirection) ? activeColor : greyColor;
    }
}