using UnityEngine;

public class Ship : MonoBehaviour
{
    public float maxRotationSpeed = 50f;

    private float rotationSpeed;
    private float previousRotationSpeed;
    private float rotationSpeedAcceleration;

    public float TargetDirection { get; set; }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.SetRotationSpeedTowardsDirection(this.TargetDirection);
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
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, this.transform.rotation.eulerAngles.z + rotationSpeed));
    }
}
