using UnityEngine;

public class Ship : MonoBehaviour
{
    public float MaxRotationSpeed = 200f;
    public float MaxRotationAcceleration = 1f;
    public AudioClip MovementSound;

    private float rotationSpeed;
    private float previousRotationSpeed;

    public float RotationSpeedAcceleration { get; private set; }
    public Vector2 RelativeTranslationAcceleration { get; private set; }

    public Vector2 Speed { get { return this.playerMovement.Speed; } }

    private float targetDirection;

    private PlayerMovement playerMovement;
    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        this.playerMovement = this.transform.parent.gameObject.GetComponent<PlayerMovement>();
        this.ConfigureAudioSource();
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
        this.UpdateSound();
	}

    private void ConfigureAudioSource()
    {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.clip = this.MovementSound;
        this.audioSource.volume = 1f;
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
        var lerpedRotationSpeed = Tools.GetLerpedRotationDelta(this.transform.rotation.eulerAngles.z, direction, 1f, this.MaxRotationSpeed);

        this.RotationSpeedAcceleration = Mathf.Clamp(lerpedRotationSpeed - this.previousRotationSpeed, -this.MaxRotationAcceleration, this.MaxRotationAcceleration);

        this.rotationSpeed += this.RotationSpeedAcceleration;
        this.previousRotationSpeed = this.rotationSpeed;
    }

    private void UpdateRotation(float rotationSpeed)
    {
        this.transform.rotation = (this.transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime).AsEulerZ();
    }

    private void UpdateSound()
    {

        if (Mathf.Approximately(0, this.Speed.magnitude))
        {
            this.audioSource.volume = 0f;
        }
        else
        {
            this.audioSource.volume = 1f;
        }

        this.audioSource.pitch = this.Speed.magnitude / 50f;

        if (!this.audioSource.isPlaying)
        {
            this.audioSource.Play();
        }  
    }


}
