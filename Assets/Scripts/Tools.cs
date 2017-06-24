using UnityEngine;

public static class Tools
{ 
    public static Vector3 AddVector2(this Vector3 vector3, Vector2 vector2)
    {
        return new Vector3(vector3.x + vector2.x, vector3.y + vector2.y);
    }

    public static float Direction(this Vector2 vector)
    {
        return (Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
    }

    public static float GetLerpedRotationDelta(float currentRotation, float targetRotation, float interpolationValue, float maxRotationSpeed)
    {
        var lerpedRotation = Mathf.LerpAngle(currentRotation, targetRotation, interpolationValue);
        var rotationDelta = lerpedRotation - currentRotation;

        if (Mathf.Abs(rotationDelta) > maxRotationSpeed)
        {
            rotationDelta = Mathf.Sign(rotationDelta) * maxRotationSpeed;
        }

        return rotationDelta;
    }

    public static Quaternion AsEulerZ(this float zRotation)
    {
        return Quaternion.Euler(new Vector3(0, 0, zRotation));
    }

    public static Vector2 RadianToVector2(this float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(this float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
