using UnityEngine;

public class Ship : MonoBehaviour
{
    public float maxRotationSpeed = 50f;

    private float rotationSpeed;
    private float previousRotationSpeed;
    private float rotationSpeedAcceleration;

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
	}

    private void SetRotationSpeedTowardsDirection(float direction)
    {
        this.rotationSpeed = Tools.GetLerpedRotationDelta(this.transform.rotation.eulerAngles.z, direction, 1f, this.maxRotationSpeed);
        this.rotationSpeedAcceleration = this.rotationSpeed - this.previousRotationSpeed;
        this.previousRotationSpeed = this.rotationSpeed;
    }

    private void UpdateRotation(float rotationSpeed)
    {
        rotationSpeed = rotationSpeed * Time.deltaTime;
        this.transform.rotation = (this.transform.rotation.eulerAngles.z + rotationSpeed).AsEulerZ();
    }


}
