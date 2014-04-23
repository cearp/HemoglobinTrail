using UnityEngine;
using System.Collections;

public class BulletUpgradeScript : PowerupScript {

	public GameObject getBullet;

	void OnCollisionEnter2D(Collision2D collision){

		WeaponScript player = collision.gameObject.GetComponent<WeaponScript> ();

		if (player != null) {
			player.shotPrefab = getBullet;
			Destroy (gameObject);
		}

	}

	void Start()
	{
		// 2 - Limited time to live to avoid any leak
		Destroy(gameObject, 10); // 5sec
	}
	
}
