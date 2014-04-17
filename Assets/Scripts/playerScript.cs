using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class playerScript : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(0, 0);
	public int invulnerable = 0;	//Not invincible at the start of course
	public int iFrames = 30;
	public int respawning = 160;
	
	// 2 - Store the movement
	private Vector2 movement;
	private int currentBullet = 0;
	
	void Update()
	{

		if (invulnerable < respawning) { 

			// 3 - Retrieve axis information
			float inputX = Input.GetAxis ("Horizontal");
			float inputY = Input.GetAxis ("Vertical");

			// 4 - Movement per direction
			movement = new Vector2 (
			speed.x * inputX,
			speed.y * inputY);

			// 5 - Shooting
			bool shoot = Input.GetButton ("Fire1");
			shoot |= Input.GetButton ("Fire2");
			// Careful: For Mac users, ctrl + arrow is a bad idea

			if (shoot) {
					WeaponScript weapon = GetComponent<WeaponScript> ();
					if (weapon != null) {
							// false because the player is not an enemy

							weapon.setInheritSpeed (new Vector2 (rigidbody2D.velocity.x / 2, Mathf.Max (rigidbody2D.velocity.y, 0)));
							weapon.Attack (false);
					}
			}

			// 6 - Make sure we are not outside the camera bounds
			var dist = (transform.position - Camera.main.transform.position).z;
	
			var leftBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (0, 0, dist)
			).x;

			var rightBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (1, 0, dist)
			).x;

			var topBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (0, 0, dist)
			).y;

			var bottomBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (0, 1, dist)
			).y;

			transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp (transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);
	
			// End of the update method
		}
	}

	public void setCurrBull(int curr){
		currentBullet = curr;
	}

	public int getCurrBull(){
		return currentBullet;
	}
	
	void FixedUpdate()
	{
		// 5 - Move the game object
		rigidbody2D.velocity = movement;
		if (invulnerable > 0) {
				
			invulnerable--;
			if (invulnerable <= respawning) 
				gameObject.renderer.enabled = !gameObject.renderer.enabled;
			
			
		}  else if (invulnerable == 0) {
				invulnerable--;
				gameObject.collider2D.enabled = true;
		}

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;
		
		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (invulnerable <= 0) {
			if (enemy != null) {
					// Kill the enemy
					HealthScript enemyHealth = enemy.GetComponent<HealthScript> ();
					if (enemyHealth != null)
							enemyHealth.Damage (enemyHealth.hp);

					damagePlayer = true;
			}

			// Damage the player
			if (damagePlayer) {
					invulnerable+= iFrames;
					gameObject.collider2D.enabled = false;
					HealthScript playerHealth = this.GetComponent<HealthScript> ();
					if (playerHealth != null)
							playerHealth.Damage (1);
			}
		}
	}
}