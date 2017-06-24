using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcceleration = 0.1f;
    private float translationAcceleration;
    private GameObject actualDirectionArrow;

    public Vector2 Speed { get; private set; }

    // Use this for initialization
    void Start ()
    {
        this.actualDirectionArrow = this.transform.Find("Radar/ActualDirectionArrow").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var leftThumbstick = InputManager.GetLeftThumbstick();
        var leftTrigger = InputManager.GetLeftTrigger();

        if (leftTrigger == 0)
        {
            this.SetSpeedTowardsVector(leftThumbstick);
        }
        else
        {
            this.Decelerate(leftTrigger);
        }

        this.UpdatePosition();
    }

    private void Decelerate(float leftTrigger)
    {
        if (Mathf.Approximately(0f, this.Speed.magnitude))
        {
            this.Speed = Vector2.zero;
            return;
        }

        var deceleration = leftTrigger * this.maxAcceleration * Time.deltaTime;
        var newSpeed = Mathf.Clamp(this.Speed.magnitude - deceleration, 0, this.Speed.magnitude);
        var newSpeedRatio = newSpeed / this.Speed.magnitude;
        this.Speed = new Vector2(this.Speed.x * newSpeedRatio, this.Speed.y * newSpeedRatio);
    }

    private void SetSpeedTowardsVector(Vector2 inputVector)
    {
        var acceleration = inputVector * this.maxAcceleration * Time.deltaTime;
        this.Speed += acceleration;
    }

    private void UpdatePosition()
    {
        this.transform.position = this.transform.position.AddVector2(this.Speed);
    }

    private float? GetDirectionFromThumbStickVector2(Vector2 vector)
    {
        if (vector.magnitude < 0.5f) return null;
        return vector.Direction();
    }
}
