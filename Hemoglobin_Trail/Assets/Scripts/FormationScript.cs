using UnityEngine;
using System.Collections;

public class FormationScript : MonoBehaviour {

	public GameObject[] enemies;
	public int scoreBonus;
	public GameObject powerUp;
	public bool droppedPup = false;
	
	private int enemyCount = 0;
	private int formationID = 1;

	public void reduceEnemyCount(){
		enemyCount--;
	}

	public void incrementEnemyCount(){
		enemyCount++;
	}

	public int getEnemyCount(){
		return enemyCount;
	}

	public void setID(int newID){
		formationID = newID;
		}

	public int getID (){
		return formationID;
	}

}
