using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class GameManagerScript : MonoBehaviour
{
	
	public int Lives = 3;
	public int Stage = 1;
	public int Score = 0;
	public int ScrollSpeed = 1;
	public int wavesCleared = 0;
	public int enemiesDead = 0;

	public List<GameObject> waves = new List<GameObject>();
	private static GameManagerScript instance;

	public void addWave(){
		GameObject go = Instantiate (Resources.Load("Prefabs/FormHolder")) as GameObject;
		waves.Add (go);
	}

	public static GameManagerScript Instance {
		get {
			if (instance == null) {
				Debug.Log("Instance null, creating new GameManager");
				instance = new GameObject("GameManager").AddComponent<GameManagerScript>();
			}
			return instance;
		}
	}

	// Sets instance to null when the application quits
	public void OnApplicationQuit() {
		instance = null;
	}

	public void incrementScore(int boost){
		Score += boost;
	}
}
