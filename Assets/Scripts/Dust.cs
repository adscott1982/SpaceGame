using System;
using UnityEngine;

public class Dust : MonoBehaviour
{
    public Vector2 Speed { get; set; }
    private PlayerMovement playerMovement;
    private Sprite dustSprite;

	// Use this for initialization
	void Start ()
    {
        this.playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        this.dustSprite = this.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = this.transform.position.AddVector2(this.Speed * Time.deltaTime);
        this.UpdateMotionBlur();
        
	}

    private void UpdateMotionBlur()
    {
        this.transform.rotation = this.playerMovement.Speed.Direction().AsEulerZ();
        var spriteWidth = this.dustSprite.bounds.size.x;
        var newWidth = spriteWidth + (this.playerMovement.Speed.magnitude / dustSprite.pixelsPerUnit);
        var blurScale = newWidth / spriteWidth;

        //this.transform.localScale.Set(blurScale, 1, 1);
        this.transform.localScale = new Vector3(blurScale, 1, 1);
    }
}
