using UnityEngine;

public class Dust : MonoBehaviour
{
    public Vector2 Speed { get; set; }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = this.transform.position.AddVector2(this.Speed * Time.deltaTime) ;
	}
}
