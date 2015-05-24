using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

//	LevelDatabase levelDatabaseScript;
//	SpawnBoss spawnBossScript;
//	public float newPhaseTimer;
//	
//	private GameObject enemyInstance;
//	private int currentLevel;
//	private int phaseTotal;
//	private float newPhaseTimerStore;
//
//	/*Increments with enemy spawns.Resets at half-phase.Determines how much you add to endPos
//Look for a better name*/
//	private int endPosAdjustment;
//
//	void spawnEnemy(GameObject enemyPrefab)
//	{
//		if(enemyPrefab.tag == "Basic")
//		{
//			//Debug.Log("Spawned a basic!");
//			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBasic, transform.position, Quaternion.identity);
//			enemyInstance.name = "Enemy01";
//		}
//		else if(enemyPrefab.tag == "Cone")
//		{
//			//Debug.Log ("Spawned a cone!");
//			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyCone, transform.position, Quaternion.identity);
//			enemyInstance.name = "Enemy02";
//		}
//	}
//
//	// Use this for initialization
//	void Start () {
//		levelDatabaseScript = GetComponent<LevelDatabase>();
//		spawnBossScript = GetComponent<SpawnBoss> ();
//		newPhaseTimerStore = newPhaseTimer;
//		spawnEnemy();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
