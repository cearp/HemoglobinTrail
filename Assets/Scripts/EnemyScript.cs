using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
	public int formationID;
	public GameObject powerUp;
	public int formScoreBoost = 0;
	public int scoreValue;

	private bool hasSpawn;
	private MoveScript moveScript;
	private WeaponScript[] weapons;
	
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();
		
		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<MoveScript>();
	}
	
	// 1 - Disable everything
	void Start()
	{
		hasSpawn = false;

		if (GameManagerScript.Instance.waves.Count < formationID + 1) {
			while (GameManagerScript.Instance.waves.Count < formationID + 1)
				GameManagerScript.Instance.addWave ();

			FormationScript wave = GameManagerScript.Instance.waves [formationID].GetComponent<FormationScript>();
			wave.powerUp = powerUp;
			wave.incrementEnemyCount ();
			wave.scoreBonus = formScoreBoost;
		} else {
			FormationScript wave = GameManagerScript.Instance.waves[formationID].GetComponent<FormationScript>();
			wave.powerUp = powerUp;
			wave.incrementEnemyCount ();
			wave.scoreBonus = formScoreBoost;
		}

		// Disable everything
		// -- collider
		collider2D.enabled = false;
		// -- Moving
		moveScript.enabled = true;
		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}
	}
	
	void Update()
	{
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
	}
}