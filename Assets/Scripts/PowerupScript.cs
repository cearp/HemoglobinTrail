using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour {

	public Vector2 speed = new Vector2(0, 0);

	protected float speedMod = 1;
	protected Vector2 movement;

	// Use this for initialization
	void Start () {
		speedMod = Mathf.Sin (Time.time);
	}
	
	// Update is called once per frame
	void Update () {
		speedMod = Mathf.Sin (Time.time);
		movement = new Vector2 (speed.x * speedMod, speed.y);

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);
	}

	void FixedUpdate()
	{
		// 5 - Move the game object
		rigidbody2D.velocity = movement;
	}
	
}
