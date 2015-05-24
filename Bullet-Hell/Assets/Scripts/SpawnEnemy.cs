using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

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

//	void whichLevel()
//	{
//		switch(currentLevel)
//		{
//		case 0:
//			levelDatabaseScript.Level0();
//			break;
//
//		case 1:
//			levelDatabaseScript.Level1();
//			phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase];
//			break;
//
//		case 2:
//			levelDatabaseScript.Level2();
//			break;
//
//		default:
//			Debug.LogError("Error!\n" + "Array Lenght: " + levelDatabaseScript.levelArray.Length 
//			               + "\n" + "Current Level: " + levelDatabaseScript.currentLevel
//			               + "\n" + "Current Level Phase: " + levelDatabaseScript.currentLevelPhase);
//			break;
//		}
//	}

	void moveToNextPhase()
	{
		if(levelDatabaseScript.currentLevelPhase > levelDatabaseScript.levelArray.Length)
		{
			if(this.transform.childCount == 0 && GameObject.Find("BossSpawnPoint").transform.childCount == 0)
			{
				levelDatabaseScript.currentLevel++;
				levelDatabaseScript.currentLevelPhase = 0;
				Debug.Log("Level: " + levelDatabaseScript.currentLevel);
				phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase];
				Application.LoadLevel("Level2");
			}
		}
		else
		{
			levelDatabaseScript.currentLevelPhase++;
			Debug.Log("Phase: " + levelDatabaseScript.currentLevelPhase);
			phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase];
		}

		newPhaseTimer = newPhaseTimerStore;
	}

	void spawnEnemy()
	{
		//Debug.Log("Entered spawnEnemy!");
		//whichLevel();
		for(int i = 0; i < phaseTotal/2; i++)
		{
			if(i < phaseTotal/2 - 1)
			{
				//Debug.Log("Spawned a basic!");
				enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBasic, transform.position, Quaternion.identity);
				enemyInstance.name = "Enemy01";
			}
			else
			{
				//Debug.Log ("Spawned a cone!");
				enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyCone, transform.position, Quaternion.identity);
				enemyInstance.name = "Enemy02";
			}
			enemyInstance.transform.parent = transform;
		}
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase>();
		spawnBossScript = GetComponent<SpawnBoss> ();
		//Temporary until you create the menu
		//currentLevel = 1;
		newPhaseTimerStore = newPhaseTimer;
		spawnEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		newPhaseTimer -= Time.deltaTime;
		if(newPhaseTimer <= 0)
		{
			moveToNextPhase();
			if(levelDatabaseScript.currentLevelPhase != levelDatabaseScript.levelArray.Length - 1)
			{
				spawnEnemy();
			}
		}
	}
}
