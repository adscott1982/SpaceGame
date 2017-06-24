using UnityEngine;

public class ActualDirectionArrow : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer arrowSprite;

	// Use this for initialization
	void Start ()
    {
        this.playerMovement = this.transform.parent.parent.gameObject.GetComponent<PlayerMovement>();
        this.arrowSprite = this.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.UpdateSpriteDirectionAndVisibility();
    }

    private void UpdateSpriteDirectionAndVisibility()
    {
        if (this.playerMovement.Speed.magnitude == 0)
        {
            this.arrowSprite.enabled = false;
            return;
        }

        this.arrowSprite.enabled = true;
        this.transform.rotation = this.playerMovement.Speed.Direction().AsEulerZ();
    }
}
