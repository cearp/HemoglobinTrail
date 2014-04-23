﻿using UnityEngine;
using System.Collections;

public class RandomSpawnScript : SpawnerScript {
	
	
	public Vector2[] velocity;	//Which direction to shoot it in
	
	private int currentBullet;	//For counting
	private playerScript player; //For counting
	
	
	// Use this for initialization
	void Start () {
		if (!isEnemy) {
				player = GameObject.Find ("player").GetComponent<playerScript> ();
				currentBullet = player.getCurrBull ();
		}
		if (lifetime == 0)
			Update ();
		if (lifetime < 999)
			Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		if (coolDown <= 0) {

			currentBullet = Mathf.FloorToInt(Random.Range(0, spawnMe.Length));
			while (currentBullet == player.getCurrBull())
				currentBullet = Mathf.FloorToInt(Random.Range(0, spawnMe.Length));
			currentBullet = currentBullet % spawnMe.Length;
			
			GameObject go = Instantiate(spawnMe[currentBullet]) as GameObject;
			if (isEnemy){
				ShotScript goShot = go.GetComponent<ShotScript> ();
				if (goShot != null)	
					goShot.isEnemyShot = isEnemy;
			}
			SpawnerScript ifSpawner = go.GetComponent<SpawnerScript>();
			
			if (ifSpawner == null){
				MoveScript moveSpeed = go.GetComponent<MoveScript>();
				moveSpeed.speed += inheritSpeed;
				moveSpeed.speed += velocity[Mathf.FloorToInt(Random.Range(0, velocity.Length))];
			} else {
				ifSpawner.setInheritSpeed(inheritSpeed);
			}
			go.transform.position = new Vector3 (transform.position.x + spawnOffset[currentBullet].x, transform.position.y + spawnOffset[currentBullet].y, transform.position.z);
			coolDown = delay;
			player.setCurrBull(currentBullet);
			if (destroySelf) Destroy(gameObject);
		} else
			coolDown -= Time.deltaTime;
	}
}
