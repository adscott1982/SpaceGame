using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float RoundsPerSecond = 5f;

    private float thumbstickActivationMag = 0.7f;
    private float timeToFire = 0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.UpdateTimeToFire();

        var rightThumbstick = InputManager.GetRightThumbstick();

        if (rightThumbstick.magnitude > this.thumbstickActivationMag)
        {
            this.RotateToDirection(rightThumbstick.Direction());

            if (this.CanFire())
            {
                this.FireWeapon();
            }
        }
	}

    private bool CanFire()
    {
        return this.timeToFire <= 0f;
    }

    private void RotateToDirection(float direction)
    {
        this.transform.rotation = direction.AsEulerZ();
    }

    private void UpdateTimeToFire()
    {
        this.timeToFire = this.timeToFire <= 0f ? 0f : this.timeToFire - Time.deltaTime;
    }

    private void FireWeapon()
    {
        this.timeToFire = 1f / this.RoundsPerSecond;
        var projectile = Instantiate(this.ProjectilePrefab, this.transform);
    }
}
