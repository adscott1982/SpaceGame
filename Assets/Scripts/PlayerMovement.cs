using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcceleration = 0.1f;

    public Vector2 Speed { get; private set; }
    public Vector2 TranslationAcceleration { get; private set; }

    // Use this for initialization
    void Start ()
    {

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
            this.TranslationAcceleration = Vector2.zero;
            return;
        }

        var deceleration = leftTrigger * this.maxAcceleration;
        var newSpeed = Mathf.Clamp(this.Speed.magnitude - deceleration, 0, this.Speed.magnitude);
        var newSpeedRatio = newSpeed / this.Speed.magnitude;

        this.TranslationAcceleration = (new Vector2(this.Speed.x * newSpeedRatio, this.Speed.y * newSpeedRatio)) - this.Speed;
    }

    private void SetSpeedTowardsVector(Vector2 inputVector)
    {
        this.TranslationAcceleration = inputVector * this.maxAcceleration;
    }

    private void UpdatePosition()
    {
        this.Speed += this.TranslationAcceleration;
        this.transform.position = this.transform.position.AddVector2(this.Speed * Time.deltaTime);
    }

    private float? GetDirectionFromThumbStickVector2(Vector2 vector)
    {
        if (vector.magnitude < 0.5f) return null;
        return vector.Direction();
    }
}
