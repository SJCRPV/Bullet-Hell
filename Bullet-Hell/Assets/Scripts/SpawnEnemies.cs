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
	private Vector3 endPosition;
	private Vector3 startPosition;
	private int phaseTotal;

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

	void spawnEnemy(int objectToSpawn)
	{
		if(objectToSpawn.tag == "Basic")
		//if(gameObject.tag == "Basic")
		{
			//Debug.Log("Spawned a basic!");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBasic, startPosition, Quaternion.identity);
			enemyInstance.name = "Basic";
		}
		else if(objectToSpawn.tag == "Cone")
		//else if(gameObject.tag == "Cone")
		{
			//Debug.Log ("Spawned a cone!");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyCone, startPosition, Quaternion.identity);
			enemyInstance.name = "Cone";
		}
	}

	void moveToNextPhase()
	{
		if(levelDatabaseScript.currentLevelPhase < levelDatabaseScript.levelArray.Length)
		{
			levelDatabaseScript.currentLevelPhase++;
			phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase];
		}

		newPhaseTimer = newPhaseTimerStore;
	}

	void spawnPattern()
	{
		for(int i = 0; i < phaseTotal; i++)
		{
			if(startPosition == this.transform && i < phaseTotal/2 - 1)
			{
				spawnEnemy (0);
			}
			else if(startPosition == this.transform && i == phaseTotal/2 - 1)
			{
				spawnEnemy(levelDatabaseScript.enemyCone);
			}
			else if(
		}
	}

	void setDestination()
	{
		if(startPosition == this.transform)
		{
			endPosition = GameObject.Find("EnemyEndPoint1").transform + adjustmentToEndPosition();
			Debug.Log(gameObject.name + " which is number " + endPosAdjustment +  " is moving to: " + endPosition);
		}
		else if(startPosition == GameObject.Find("EnemySpawnPoint2").transform)
		{
			endPosition = GameObject.Find("EnemyEndPoint2").transform - adjustmentToEndPosition();
			Debug.Log(gameObject.name + " which is number " + endPosAdjustment +  " is moving to: " + endPosition);
		}
		else
		{
			Debug.LogError(gameObject.name + " which is number " + endPosAdjustment + " does not have a destination");
		}
	}

	void setStartingPoint()
	{
		for(int i = 0; i < phaseTotal; i++)
		{
			if(i < phaseTotal/2)
			{
				startPosition = this.transform;
			}
			else
			{
				startPosition = GameObject.Find("EnemySpawnPoint2").transform;
			}
		}
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase>();
		spawnBossScript = GetComponent<SpawnBoss>();
		newPhaseTimerStore = newPhaseTimer;
		setStartingPoint();
		setDestination();
		spawnPattern();
	}
	
	// Update is called once per frame
	void Update () {
		newPhaseTimer -= Time.deltaTime;
		if(newPhaseTimer <= 0)
		{
			moveToNextPhase();
		}
	}
}
