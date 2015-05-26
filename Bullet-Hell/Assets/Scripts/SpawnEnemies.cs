using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	LevelDatabase levelDatabaseScript;
	SpawnBoss spawnBossScript;
	public float newPhaseTimer;
	
	private GameObject enemyInstance;
	private int currentLevel;
	private int phaseTotal;
	private float newPhaseTimerStore;

	/*Increments with enemy spawns.Resets at half-phase.Determines how much you add to endPos
Look for a better name*/
	private int endPosAdjustment;

	public Vector3 adjustmentToEndPosition()
	{
		Vector3 addOn;
		
		if(endPosAdjustment * 0.8f > 5)
		{
			endPosAdjustment = 0;
			addOn = new Vector3( 0.8f * endPosAdjustment, 0.8f, 0);
		}
		else
		{
			addOn = new Vector3( 0.8f * endPosAdjustment, 0, 0);
		}
		
		endPosAdjustment++;
		return addOn;
	}

	void spawnEnemy()
	{
		if(gameObject.tag == "Basic")
		{
			//Debug.Log("Spawned a basic!");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBasic, transform.position, Quaternion.identity);
			enemyInstance.name = "Enemy01";
		}
		else if(gameObject.tag == "Cone")
		{
			//Debug.Log ("Spawned a cone!");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyCone, transform.position, Quaternion.identity);
			enemyInstance.name = "Enemy02";
		}
	}

	void setDestination()
	{
		if(endPosAdjustment < levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase]/2)
		{

		}
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase>();
		spawnBossScript = GetComponent<SpawnBoss> ();
		newPhaseTimerStore = newPhaseTimer;
		setDestination();
		spawnEnemy();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
