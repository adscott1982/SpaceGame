using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float LifeTime = 5f;
    public float Speed = 2f;

    private Vector2 speedVector;

	// Use this for initialization
	void Start ()
    {
        this.transform.SetParent(null, true);
        this.speedVector = this.transform.rotation.eulerAngles.z.DegreeToVector2() * this.Speed;  
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.DeductTimeFromLife();

        if (this.LifeTime < 0)
        {
            Destroy(this.gameObject);
        }

        this.UpdatePosition();
	}

    private void UpdatePosition()
    {
        this.transform.position = this.transform.position.AddVector2(this.speedVector * Time.deltaTime);
    }

    private void DeductTimeFromLife()
    {
        this.LifeTime -= Time.deltaTime;
    }
}
