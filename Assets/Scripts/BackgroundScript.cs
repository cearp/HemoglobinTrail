using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public float water = 0.05f;

	private GameObject[] oceans;
	private Color blood;

	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
	void Update () {
		oceans = GameObject.FindGameObjectsWithTag ("background");
		foreach (GameObject ocean in oceans) {
			blood = ocean.renderer.material.color;
			if (blood.a < 1f) {
				blood.a += water;
				ocean.renderer.material.color = blood;
				Debug.Log (blood.a);
			} else {
				Debug.Log("full opacity");
			}
		}
	}
}
