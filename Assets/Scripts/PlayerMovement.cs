using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcceleration = 0.1f;
    public float maxRotationSpeed = 50f;

    private Vector2 speed = Vector2.zero;
    private float translationAcceleration;

    private float rotationSpeed;
    private float previousRotationSpeed;
    private float rotationSpeedAcceleration;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var leftThumbstick = GetThumbstick();
        this.SetSpeed(leftThumbstick);
        this.SetRotationSpeedTowardsVector(this.speed);
        this.UpdateTransform();
    }

    private Vector2 GetThumbstick()
    {
        var vector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        return vector; 
    }

    private void SetSpeed(Vector2 inputVector)
    {
        var acceleration = inputVector * this.maxAcceleration * Time.deltaTime;
        this.speed += acceleration;
    }

    private void SetRotationSpeedTowardsVector(Vector2 vector)
    {
        this.rotationSpeed = this.GetLerpedRotationDelta(vector, 1f, this.maxRotationSpeed);
        this.rotationSpeedAcceleration = this.rotationSpeed - this.previousRotationSpeed;
        this.previousRotationSpeed = this.rotationSpeed;
    }

    private void UpdateTransform()
    {
        this.transform.position = this.transform.position.AddVector2(this.speed);
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, this.transform.rotation.eulerAngles.z + this.rotationSpeed));
    }

    private float? GetDirectionFromThumbStickVector2(Vector2 vector)
    {
        if (vector.magnitude < 0.5f) return null;
        return (Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg) - 90f;
    }

    private float GetLerpedRotationDelta(Vector2 vector, float interpolationValue, float maxRotationDelta)
    {
        var targetRotation = (Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
        var currentRotation = this.transform.rotation.eulerAngles.z;
        var maxRotationThisFrame = this.maxRotationSpeed * Time.deltaTime;
        var lerpedRotation = Mathf.LerpAngle(currentRotation, targetRotation, interpolationValue * Time.deltaTime);
        var rotationDelta = lerpedRotation - currentRotation;

        if (Mathf.Abs(rotationDelta) > maxRotationThisFrame)
        {
            rotationDelta = Mathf.Sign(rotationDelta) * maxRotationThisFrame;
        }

        return rotationDelta;
    }
}
