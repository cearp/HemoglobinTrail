using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

	public GameObject[] spawnMe;	//What prefab to spawn
	public Vector3[] spawnOffset;	//Where does each matching prefab spawn?
	public float delay;			//How long until I spawn it
	public bool destroySelf;	//Do I need to destroy the spawner after one spawn
	public float lifetime = 999;		//Destroy the spawner after lifetime is up
	public bool isEnemy;

	protected Vector2 inheritSpeed;
	protected float coolDown = 0; 	//For counting


	// Use this for initialization
	void Start () {
		if (lifetime == 0)
			Update ();
		if (lifetime < 999f) 
			Destroy (gameObject, lifetime);
					
				
	}

	public void setInheritSpeed(Vector2 newSpeed){
		inheritSpeed = newSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (coolDown <= 0f) {
			for (int i = 0; i < spawnMe.Length; i++) {
					GameObject go = Instantiate (spawnMe [i]) as GameObject;
					if (isEnemy) {
							ShotScript goShot = go.GetComponent<ShotScript> ();
							if (goShot != null)	
							goShot.isEnemyShot = isEnemy;
					}
					SpawnerScript ifSpawner = go.GetComponent<SpawnerScript> ();
	
					if (ifSpawner == null) {
							MoveScript moveSpeed = go.GetComponent<MoveScript> ();
							moveSpeed.speed += inheritSpeed;
					} else {
							ifSpawner.setInheritSpeed (inheritSpeed);
					}
					go.transform.position = new Vector3 (transform.position.x + spawnOffset [i].x, transform.position.y + spawnOffset [i].y, transform.position.z);
			}
			coolDown = delay;
			if (destroySelf){
					Destroy (gameObject);
			}
			
		} else 
			coolDown -= Time.deltaTime;
		
	}
}
