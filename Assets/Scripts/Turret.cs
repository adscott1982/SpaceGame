using UnityEngine;

public class Turret : MonoBehaviour
{
    private float thumbstickActivationMag = 0.7f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var rightThumbstick = InputManager.GetRightThumbstick();

        if (rightThumbstick.magnitude > this.thumbstickActivationMag)
        {
            this.RotateToDirection(rightThumbstick.Direction());
            this.FireWeapon();
        }
	}

    private void RotateToDirection(float direction)
    {
        this.transform.rotation = direction.AsEulerZ();
    }

    private void FireWeapon()
    {

    }
}
