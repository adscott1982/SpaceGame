using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 5f;
    private float speed = 0f;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private float GetThumbstickDirection(string axis)
    {
        direction = Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
}
