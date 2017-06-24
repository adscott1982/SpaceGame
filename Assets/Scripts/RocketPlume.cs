using System.Collections.Generic;
using UnityEngine;
using Enums;

public class RocketPlume : MonoBehaviour
{
    public List<SpriteRenderer> RocketSprites;
    public List<ThrustTypes> ThrustTypes;
    public float TranslationScale;
    public float RotationScale;

    private Ship ship;

	// Use this for initialization
	void Start ()
    {
		this.ship = this.transform.parent.gameObject.GetComponent<Ship>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        var translationAccelerationX = this.ship.RelativeTranslationAcceleration.x;
        var translationAccelerationY = this.ship.RelativeTranslationAcceleration.y;
        var rotationAcceleration = this.ship.RotationSpeedAcceleration;

        var totalThrustRatio = 0f;

        foreach(var thrustType in this.ThrustTypes)
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
        this.UpdatePlumeScale(totalThrustRatio);
    }

    private void UpdatePlumeScale(float acceleration)
    {
        var xScale = 0f;
        
        if (acceleration > 0 && !Mathf.Approximately(0, acceleration))
        {
            xScale = (acceleration) * 10;
        }

        foreach (var sprite in this.RocketSprites)
        {
            var currentScale = sprite.gameObject.transform.localScale;
            sprite.gameObject.transform.localScale = new Vector3(xScale, currentScale.y, currentScale.z);
        }
    }

    private float GetThrustScale(float value, bool isPositive, float scaleMax)
    {
        if (value < 0 && isPositive || value > 0 && !isPositive)
        {
            return 0f;
        }

        return Mathf.Abs(value) / scaleMax;
    }
}
