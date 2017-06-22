using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcceleration = 0.1f;
    public float maxRotationSpeed = 0.1f;

    private Vector2 speed = Vector2.zero;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var leftThumbstick = GetThumbstick();
        var direction = GetDirectionFromThumbStickVector2(leftThumbstick);
        this.SetSpeed(leftThumbstick);
        this.SetRotation(this.speed);
        this.UpdatePosition();
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

    private void UpdatePosition()
    {
        this.transform.position = this.transform.position.AddVector2(this.speed);
    }

    private float? GetDirectionFromThumbStickVector2(Vector2 vector)
    {
        if (vector.magnitude < 0.5f) return null;
        return (Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg) - 90f;
    }

    private void SetRotation(float direction)
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }

    private void SetRotation(Vector2 vector)
    {
        var targetRotation = (Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
        var currentRotation = this.transform.rotation.eulerAngles.z;
        var maxRotationThisFrame = this.maxRotationSpeed * Time.deltaTime;

        var lerpedRotation = Mathf.LerpAngle(currentRotation, targetRotation, 1f * Time.deltaTime);
        var rotationDelta = lerpedRotation - currentRotation;

        if (Mathf.Abs(rotationDelta) > maxRotationThisFrame)
        {
            rotationDelta = Mathf.Sign(rotationDelta) * maxRotationThisFrame;
        }

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRotation + rotationDelta));
    }
}
