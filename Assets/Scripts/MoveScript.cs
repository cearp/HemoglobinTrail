using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class MoveScript : MonoBehaviour
{
	// 1 - Designer variables
	
	/// <summary>
	/// Object speed
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);
	public float turnSpeed = 1f;
	
	/// <summary>
	/// Moving direction
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);
	
	protected Vector2 movement;
	
	void Update()
	{
		// 2 - Movement
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
	}
	
	void FixedUpdate()
	{
		// Apply movement to the rigidbody
		rigidbody2D.velocity = movement;

		float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
		
		// actually rotates the sprite using Slerp (from its previous rotation, to the new one at the designated speed.
		
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
	}
}