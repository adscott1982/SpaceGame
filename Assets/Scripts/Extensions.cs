using UnityEngine;

public static class Extensions
{ 
    public static Vector3 AddVector2(this Vector3 vector3, Vector2 vector2)
    {
        return new Vector3(vector3.x + vector2.x, vector3.y + vector2.y);
    }

    public static float Direction(this Vector2 vector)
    {
        return (Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
    }
}
