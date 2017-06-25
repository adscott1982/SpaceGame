using UnityEngine;

public class Ship : MonoBehaviour
{
    public float maxRotationSpeed = 200f;
    public float maxRotationAcceleration = 1f;

    private float rotationSpeed;
    private float previousRotationSpeed;

    public float RotationSpeedAcceleration { get; private set; }
    public Vector2 RelativeTranslationAcceleration { get; private set; }

    public Vector2 Speed { get { return this.playerMovement.Speed; } }

    private float targetDirection;

    private PlayerMovement playerMovement;

    // Use this for initialization
    void Start ()
    {
        this.playerMovement = this.transform.parent.gameObject.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.playerMovement.Speed.magnitude > 0)
        {
            this.targetDirection = this.playerMovement.Speed.Direction();
        }

        this.SetRotationSpeedTowardsDirection(this.targetDirection);
        this.UpdateRotation(this.rotationSpeed);
        this.UpdateRelativeTranslationAcceleration();
	}

    private void UpdateRelativeTranslationAcceleration()
    {
        if (Mathf.Approximately(0f, this.playerMovement.TranslationAcceleration.magnitude))
        {
            this.RelativeTranslationAcceleration = Vector2.zero;
            return;
        }
        
        var directionAsVector = this.transform.rotation.eulerAngles.z.DegreeToVector2();
        var angleToRelativeAcceleration = Vector2.Angle(directionAsVector, this.playerMovement.TranslationAcceleration);
        this.RelativeTranslationAcceleration = angleToRelativeAcceleration.DegreeToVector2() * this.playerMovement.TranslationAcceleration.magnitude;
    }

    private void SetRotationSpeedTowardsDirection(float direction)
    {
        var lerpedRotationSpeed = Tools.GetLerpedRotationDelta(this.transform.rotation.eulerAngles.z, direction, 1f, this.maxRotationSpeed);

        this.RotationSpeedAcceleration = Mathf.Clamp(lerpedRotationSpeed - this.previousRotationSpeed, -this.maxRotationAcceleration, this.maxRotationAcceleration);

        this.rotationSpeed += this.RotationSpeedAcceleration;
        this.previousRotationSpeed = this.rotationSpeed;
    }

    private void UpdateRotation(float rotationSpeed)
    {
        this.transform.rotation = (this.transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime).AsEulerZ();
    }


}
