using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public float water = 0.05f;

	private bool canMoveOn = false;
	private GameObject[] oceans;
	private Color blood;

	// Use this for initialization
//	void Start () {
//	
//	}
	private void finish(){

		canMoveOn = true;
		GameObject go = new GameObject ();
		go.AddComponent<nextLevelScript> ();
		go.transform.position = transform.position;
		playerScript temp = FindObjectOfType<playerScript>();
		if (temp != null) {
			temp.setSpeed(new Vector2(0, 32));
			temp.invulnerable = 999;
		}
		GameManagerScript.Instance.kaboom();

	}
	
	// Update is called once per frame
	void Update () {
		oceans = GameObject.FindGameObjectsWithTag ("background");
		foreach (GameObject ocean in oceans) {
			blood = ocean.renderer.material.color;
			if (blood.a < 1f) {

				if (blood.a <= 0 && !canMoveOn){
					finish();
				} else {
					blood.a += water;
					ocean.renderer.material.color = blood;
				}

			} else {
				Debug.Log("full opacity");
			}
		}
	}
}
