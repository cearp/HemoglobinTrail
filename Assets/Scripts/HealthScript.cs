using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
	/// <summary>
	/// Total hitpoints
	/// </summary>
	public float hp = 1;
	public int iFrames = 320;
	
	/// <summary>
	/// Enemy or player?
	/// </summary>
	public bool isEnemy = true;

	//-----------------------Added stuff for background-----------------------//
	public float enemyRed = 0.1f;  //amount the water will be red per enemy
	private int enemyDeath;
	
	private GameObject[] oceans;
	private Color blood;
	//--
	
	/// <summary>
	/// Inflicts damage and check if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>
	public void Damage(float damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0)
		{
			// 'Splosion!
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound();

			if (isEnemy){

				EnemyScript en = gameObject.GetComponent<EnemyScript>();
				FormationScript wave = GameManagerScript.Instance.waves[en.formationID].GetComponent<FormationScript>();


				wave.reduceEnemyCount();
				GameManagerScript.Instance.incrementScore(en.scoreValue);

				if (wave.getEnemyCount() <= 0 && !wave.droppedPup){

					if (wave.powerUp != null){
						GameObject go = Instantiate(wave.powerUp) as GameObject;
						go.transform.position = transform.position;
					}
					GameManagerScript.Instance.incrementScore(wave.scoreBonus);
					wave.droppedPup = true;
				}
			}
			else {
				if (GameManagerScript.Instance.Lives > 0){
					GameObject player = Instantiate(Resources.Load("Prefabs/player")) as GameObject;
					player.name = "player";
					playerScript p = player.GetComponent<playerScript>();
					player.transform.position = Camera.main.transform.position + new Vector3(0, 0, 8);
					p.invulnerable = iFrames;
					player.collider2D.enabled = false;
					player.renderer.enabled = false;
					GameManagerScript.Instance.Lives--;
				} else {
					// Game Over.
					// Add the script to the parent because the current game
					// object is likely going to be destroyed immediately.
					GameObject go = new GameObject();
					go.AddComponent<GameOverScript>();
					go.transform.position = transform.position;
				}
			}

			// Dead!
			//-----------------------Added stuff for background-----------------------//
			enemyDeath = GameManagerScript.Instance.enemiesDead++;
			oceans = GameObject.FindGameObjectsWithTag("background");
			
			foreach (GameObject ocean in oceans) {
				blood = ocean.renderer.material.color;
				blood.a -= enemyRed;
				ocean.renderer.material.color = blood;
			}
			//------------------------------------------------------------------------//
			Destroy(gameObject);
		}

	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);
				
				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
	}
}