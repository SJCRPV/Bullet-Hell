using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	LevelDatabase levelDatabaseScript;
	SpawnBoss spawnBossScript;
	public float newPhaseTimer;
	
	private GameObject enemyInstance;
	private int currentLevel;
	private int phaseTotal;
	private int positionInPhase;
	private float newPhaseTimerStore;
	private Vector3 endPosition;
	private Vector3 startPosition;

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
		switch(objectToSpawn)
		{
		case 0:
			//Debug.Log("Spawned a basic!");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBasic, startPosition, Quaternion.identity);
			enemyInstance.name = "Basic";
			break;

		case 1:
			//Debug.Log ("Spawned a cone!");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyCone, startPosition, Quaternion.identity);
			enemyInstance.name = "Cone";
			break;

		case 12:
			//Debug.Log("Spawned boss1");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBoss1, startPosition, Quaternion.identity);
			enemyInstance.name = "Boss1";
			break;
		}
	}

	void spawnPattern()
	{
		spawnEnemy(levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase, positionInPhase]);
	}

	void setDestination()
	{
		if(startPosition == this.transform.position)
		{
			endPosition = GameObject.Find("EnemyEndPoint1").transform.position + adjustmentToEndPosition();
			Debug.Log("Ship number " + endPosAdjustment + " is moving to: " + endPosition);
		}
		else if(startPosition == GameObject.Find("EnemySpawnPoint2").transform.position)
		{
			endPosition = GameObject.Find("EnemyEndPoint2").transform.position - adjustmentToEndPosition();
			Debug.Log("Ship number " + endPosAdjustment + " is moving to: " + endPosition);
		}
		else if(levelDatabaseScript.currentLevelPhase == 4)
		{
			endPosition = GameObject.Find("BossEndPoint").transform.position;
		}
		else
		{
			Debug.LogError("Ship number " + endPosAdjustment + " does not have a destination");
		}
		spawnPattern();
	}

	void setStartingPoint()
	{
		if(levelDatabaseScript.currentLevelPhase == 4)
		{
			startPosition = GameObject.Find("BossSpawnPoint").transform.position;
			setDestination();
			return;
		}
		for(positionInPhase = 0; positionInPhase < phaseTotal; positionInPhase++)
		{
			if(positionInPhase < phaseTotal/2)
			{
				startPosition = this.transform.position;
			}
			else
			{
				startPosition = GameObject.Find("EnemySpawnPoint2").transform.position;
			}
			setDestination();
		}
	}

	void moveToNextPhase()
	{
		if(levelDatabaseScript.currentLevelPhase < levelDatabaseScript.levelArray.Length)
		{
			levelDatabaseScript.currentLevelPhase++;
			phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase, 0];
		}
		
		newPhaseTimer = newPhaseTimerStore;
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase>();
		spawnBossScript = GetComponent<SpawnBoss>();
		newPhaseTimerStore = newPhaseTimer;
		setStartingPoint();
	}
	
	// Update is called once per frame
	void Update () {
		newPhaseTimer -= Time.deltaTime;
		if(newPhaseTimer <= 0)
		{
			moveToNextPhase();
			setStartingPoint();
		}
	}
}
