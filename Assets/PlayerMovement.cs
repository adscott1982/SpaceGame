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
        var leftThumbstick = GetThumbstick();
        //var direction = Vector2.Angle(Vector2.up, leftThumbstick);
        var direction = GetDirectionFromThumbStickVector2(leftThumbstick);

        if (direction.HasValue)
        {
            Debug.Log("X = " + leftThumbstick.x + "Y = " + leftThumbstick.y + " Direction = " + direction);
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction.Value));
        }
    }

    private Vector2 GetThumbstick()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private float? GetDirectionFromThumbStickVector2(Vector2 vector)
    {
        if (vector.magnitude < 0.5f) return null;
        return (Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg) - 90f;
    }
}
