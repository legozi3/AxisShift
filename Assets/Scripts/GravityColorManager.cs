using UnityEngine;

public class GravityColorManager : MonoBehaviour
{
    public Color downColor    = Color.blue;
    public Color upColor      = Color.red;
    public Color leftColor    = Color.green;
    public Color rightColor   = Color.yellow;
    public Color forwardColor = Color.cyan;
    public Color backColor    = Color.magenta;

    public Renderer[] objectsToColor; //the object I want to change of color of

    void Update()
    {
        Color targetColor = GetColorForDirection(GravityFlip.currentDirection);

        foreach (Renderer r in objectsToColor)
        {
            r.material.color = targetColor;
        }
    }

    Color GetColorForDirection(GravityDirection dir)
    {
        switch (dir)
        {
            case GravityDirection.Down:    return downColor;
            case GravityDirection.Up:      return upColor;
            case GravityDirection.Left:    return leftColor;
            case GravityDirection.Right:   return rightColor;
            case GravityDirection.Forward: return forwardColor;
            case GravityDirection.Back:    return backColor;
            default:                       return downColor;
        }
    }
}