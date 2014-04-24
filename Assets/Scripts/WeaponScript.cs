using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
	//--------------------------------
	// 1 - Designer variables
	//--------------------------------
	
	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public GameObject shotPrefab;
	
	//--------------------------------
	// 2 - Cooldown
	//--------------------------------
	
	private float shootCooldown;
	private Vector2 inheritSpeed;
	
	void Start()
	{
		shootCooldown = 0f;
	}
	
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}
	
	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------
	
	/// <summary>
	/// Create a new projectile if possible
	/// </summary>
	public void Attack(bool isEnemy)
	{

		if (CanAttack)
		{
			ShotScript shot = shotPrefab.GetComponent<ShotScript>();
			
			shootCooldown = Random.Range(shot.shootingRate, shot.shootingRateHigh);

			// Create a new shot
			GameObject shotObj = Instantiate(shotPrefab) as GameObject;
			SpawnerScript ifSpawner = shotObj.GetComponent<SpawnerScript>();

			HealthScript hp = shotObj.GetComponent<HealthScript>();

			if (hp != null)
				hp.isBullet = true;

			if (ifSpawner == null){
				MoveScript moveSpeed = shotObj.GetComponent<MoveScript>();
				moveSpeed.speed += inheritSpeed;
			} else {
				ifSpawner.setInheritSpeed(inheritSpeed);
			}
			
			// Assign position
			shotObj.transform.position = transform.position;
			shotObj.transform.rotation = transform.rotation;


			
			// The is enemy property
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}

			if (!shot.isEnemyShot)
				SoundEffectsHelper.Instance.MakePlayerShotSound();
			
			// Make the weapon shot always towards it
			MoveScript move = shot.gameObject.GetComponent<MoveScript>();
			if (move != null)
			{
				move.direction = this.transform.up; // towards in 2D space is the right of the sprite
			}
		}
	}

	public void setInheritSpeed(Vector2 newSpeed){
		inheritSpeed = newSpeed;
	}
	
	/// <summary>
	/// Is the weapon ready to create a new projectile?
	/// </summary>
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}