using UnityEngine;
using System.Collections;

public class BossScript : EnemyScript {

	public float decelerateTo;
	private float interval;

	// Use this for initialization
	void Start () {
		base.Init ();
		isBoss = true;
		interval = moveScript.speed.y - decelerateTo;
	}

	//Activate
	void Awake(){
		base.Wake ();
		isBoss = true;
	}

	new void shootAndCheck(){

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

		}

	}
	
	// Update is called once per frame
	void Update () {
		shootAndCheck ();

		if (moveScript.speed.y < decelerateTo){
			moveScript.speed.y = decelerateTo;}
		else {
			moveScript.speed.y -= moveScript.speed.y * Time.deltaTime / interval;
		}
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

		moveScript.speed = new Vector2 (0, moveScript.speed.y);
		moveScript.direction = new Vector2 (1, -1); 
	}

}
