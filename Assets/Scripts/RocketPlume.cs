using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

public class RocketPlume : MonoBehaviour
{
    public List<SpriteRenderer> RocketSprites;
    public List<ThrustTypes> ThrustTypes;
    public float TranslationScale;
    public float RotationScale;
    public AudioClip ThrusterSound;

    private Ship ship;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
		this.ship = this.transform.parent.gameObject.GetComponent<Ship>();
        this.ConfigureAudioSource();
    }
	
	// Update is called once per frame
	void Update ()
    {
        var thrustRatio = this.GetThrustRatio();
        this.UpdatePlumeScale(thrustRatio);
        this.UpdateThrusterSound(thrustRatio);
    }

    private void UpdateThrusterSound(float thrustRatio)
    {
        if (!this.audioSource.isPlaying)
        {
            this.audioSource.Play();
        }

        this.audioSource.volume = thrustRatio;
    }

    private void ConfigureAudioSource()
    {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.clip = this.ThrusterSound;
        this.audioSource.volume = 0f;
    }

    private float GetThrustRatio()
    {
        var translationAccelerationX = this.ship.RelativeTranslationAcceleration.x;
        var translationAccelerationY = this.ship.RelativeTranslationAcceleration.y;
        var rotationAcceleration = this.ship.RotationSpeedAcceleration;

        var totalThrustRatio = 0f;

        foreach (var thrustType in this.ThrustTypes)
        {
            switch (thrustType)
            {
                case Enums.ThrustTypes.Forward:
                totalThrustRatio += this.GetThrustScale(translationAccelerationX, true, this.TranslationScale);
                break;

                case Enums.ThrustTypes.Reverse:
                totalThrustRatio += this.GetThrustScale(translationAccelerationX, false, this.TranslationScale);
                break;

                case Enums.ThrustTypes.Left:
                totalThrustRatio += this.GetThrustScale(translationAccelerationY, true, this.TranslationScale);
                break;

                case Enums.ThrustTypes.Right:
                totalThrustRatio += this.GetThrustScale(translationAccelerationY, false, this.TranslationScale);
                break;

                case Enums.ThrustTypes.RotateCCW:
                totalThrustRatio += this.GetThrustScale(rotationAcceleration, true, this.RotationScale);
                break;

                case Enums.ThrustTypes.RotateCW:
                totalThrustRatio += this.GetThrustScale(rotationAcceleration, false, this.RotationScale);
                break;

                default:
                break;
            }
        }

        totalThrustRatio = Mathf.Clamp(totalThrustRatio, 0f, 1f);

        return totalThrustRatio;
    }

    private void UpdatePlumeScale(float totalThrustRatio)
    {
        foreach (var sprite in this.RocketSprites)
        {
            var currentScale = sprite.gameObject.transform.localScale;
            sprite.gameObject.transform.localScale = new Vector3(totalThrustRatio, currentScale.y, currentScale.z);
        }
    }

    /// <summary>
    /// Gets the scale of the thrust (0..1) corresponding to the value with respect to the scale.
    /// </summary>
    /// <param name="value">The value of the acceleration.</param>
    /// <param name="isPositive">Whether the value should be inverted.</param>
    /// <param name="scaleMax">The maximum scale.</param>
    /// <returns></returns>
    private float GetThrustScale(float value, bool isPositive, float scaleMax)
    {
        if (value < 0 && isPositive || value > 0 && !isPositive)
        {
            return 0f;
        }

        return Mathf.Abs(value) / scaleMax;
    }
}
