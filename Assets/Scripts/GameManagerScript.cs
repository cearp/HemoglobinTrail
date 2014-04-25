using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class GameManagerScript : MonoBehaviour
{
	
	public static int Lives = 3;
	public static int Stage = 1;
	public static int Score = 0;
	public static int ScrollSpeed = 1;
	public static int wavesCleared = 0;
	public static int enemiesDead = 0;
	public static GameObject currBull = null;

	public List<GameObject> waves = new List<GameObject>();
	private static GameManagerScript instance;
	private string[] stages = new string[5]{"Main_Menu", "Stage_One", "Stage_Two", "Stage_Final", "Credits"};

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

	public void submitScore(){
		Kongregate.SubmitStatistic ("High Score", Score);
	}

	public void nextLevel(){

		playerScript temp = FindObjectOfType<playerScript>();
		if (temp != null)
			currBull = temp.gameObject.GetComponent<WeaponScript>().shotPrefab;
		Debug.LogWarning (stages [Stage + 1]);

		if (Stage + 1 < stages.Length) 
			Application.LoadLevel (stages [Stage + 1]);
		Score += Stage * 1000;
		Kongregate.SubmitStatistic ("High Score", Score);
		Stage++;

	}

	public void kaboom(){

		EnemyScript[] enemies = FindObjectsOfType<EnemyScript> ();
		foreach (EnemyScript en in enemies) {
			HealthScript hs = en.GetComponent<HealthScript>();
			hs.Damage(hs.hp);
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
