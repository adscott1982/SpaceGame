using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcceleration = 0.1f;
    public float maxRotationSpeed = 50f;

    private Vector2 speed = Vector2.zero;
    private float translationAcceleration;

    private float targetDirection;
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
        var leftThumbstick = this.GetThumbstick();
        var leftTrigger = this.GetLeftTrigger();

        if (leftTrigger == 0)
        {
            this.SetSpeedTowardsVector(leftThumbstick);
        }
        else
        {
            this.Decelerate(leftTrigger);
        }

        if (this.speed.magnitude > 0)
        {
            this.targetDirection = this.speed.Direction();
            this.SetRotationSpeedTowardsDirection(this.targetDirection);
        }

        this.SetRotationSpeedTowardsDirection(this.targetDirection);

        this.UpdateTransform();
    }

    private void Decelerate(float leftTrigger)
    {
        if (Mathf.Approximately(0f, this.speed.magnitude))
        {
            this.speed = Vector2.zero;
            return;
        }

        var deceleration = leftTrigger * this.maxAcceleration * Time.deltaTime;
        var newSpeed = Mathf.Clamp(this.speed.magnitude - deceleration, 0, this.speed.magnitude);
        var newSpeedRatio = newSpeed / this.speed.magnitude;
        this.speed = new Vector2(this.speed.x * newSpeedRatio, this.speed.y * newSpeedRatio);
    }

    private Vector2 GetThumbstick()
    {
        var vector = new Vector2(Input.GetAxis("LThumbstickX"), Input.GetAxis("LThumbstickY"));
        return vector;
    }

    private float GetLeftTrigger()
    {
        var leftTriggerValue = Input.GetAxis("LeftTrigger");
        Debug.Log("Left trigger: " + leftTriggerValue);
        return leftTriggerValue;
    }

    private void SetSpeedTowardsVector(Vector2 inputVector)
    {
        var acceleration = inputVector * this.maxAcceleration * Time.deltaTime;
        this.speed += acceleration;
    }

    private void SetRotationSpeedTowardsDirection(float direction)
    {
        this.rotationSpeed = this.GetLerpedRotationDelta(direction, 1f, this.maxRotationSpeed);
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
        return vector.Direction() - 90f;
    }

    private float GetLerpedRotationDelta(float targetRotation, float interpolationValue, float maxRotationDelta)
    {
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
