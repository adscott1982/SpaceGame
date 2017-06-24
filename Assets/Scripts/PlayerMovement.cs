using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcceleration = 0.1f;

    private Vector2 speed = Vector2.zero;
    private float translationAcceleration;

    private Ship ship;
    private GameObject targetDirectionArrow;
    private GameObject actualDirectionArrow;

    // Use this for initialization
    void Start ()
    {
        this.ship = this.transform.Find("Ship").gameObject.GetComponent(typeof(Ship)) as Ship;
        this.targetDirectionArrow = this.transform.Find("TargetDirectionArrow").gameObject;
        this.actualDirectionArrow = this.transform.Find("ActualDirectionArrow").gameObject;
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
            this.ship.TargetDirection = this.speed.Direction();
        }

        this.UpdatePosition();
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

    private void UpdatePosition()
    {
        this.transform.position = this.transform.position.AddVector2(this.speed);
    }

    private float? GetDirectionFromThumbStickVector2(Vector2 vector)
    {
        if (vector.magnitude < 0.5f) return null;
        return vector.Direction() - 90f;
    }
}
