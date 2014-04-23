using UnityEngine;
using System.Collections;

public class BossScript : EnemyScript {

	// Use this for initialization
	void Start () {
		
	}

	//Activate
	void Awake(){
		base.Init ();
	}

	void shootAndCheck(){

		// 2 - Check if the enemy has spawned.
		if (hasSpawn == false)
		{
			if (renderer.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			// Auto-fire
			foreach (WeaponScript weapon in weapons)
			{
				if (weapon != null && weapon.enabled && weapon.CanAttack)
				{
					weapon.setInheritSpeed(moveScript.speed);
					weapon.Attack(true);
					//weapon.shotPrefab.shootingRate = Mathf.Floor( Random.Range(weapon.shotPrefab.shootingRate, weapon.shotPrefab.shootingRateHigh));
					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}
			
			// 4 - Out of the camera ? Destroy the game object.
			if (renderer.IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		Debug.LogWarning ("Distance: " + Vector3.Distance (FindObjectOfType<playerScript>().transform.position, transform.position));
		Debug.LogWarning ("Speed: " + moveScript.speed);
		shootAndCheck ();
	}

	// 3 - Activate itself.
	private void Spawn()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		collider2D.enabled = true;
		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
		
		moveScript.speed = new Vector2 (0, 2);
		moveScript.direction = new Vector2 (1, 1); 
	}

}
