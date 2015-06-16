using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public LevelDatabase levelDatabaseScript;
	SpawnBoss spawnBossScript;
	public float newPhaseTimer;
	public Vector3 startPosition;
	
	private GameObject enemyInstance;
    private GameObject spawnPoint1;
    private GameObject spawnPoint2;
    private GameObject bossSpawnPoint;
	private int currentLevel;
	private int phaseTotal;
	private int positionInPhase;
	private float newPhaseTimerStore;

	/*Increments with enemy spawns.Resets at half-phase.Determines how much you add to endPos
Look for a better name*/
	public float endPosAdjustment;

    void assignParent()
    {
        if (startPosition == bossSpawnPoint.transform.position)
        {
            enemyInstance.transform.parent = bossSpawnPoint.transform;
        }
		else
		{
			enemyInstance.transform.parent = spawnPoint1.transform;
		}
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

		case 2:
			//Debug.Log ("Spawned a graze!");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyGraze, startPosition, Quaternion.identity);
			enemyInstance.name = "Graze";
			break;

		case 12:
			//Debug.Log("Spawned boss1");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBoss1, startPosition, Quaternion.identity);
			enemyInstance.name = "Boss1";
			this.enabled = false;
			break;

        default:
            Debug.LogError("I did not spawn anything with the number " + objectToSpawn);
            break;
		}
        assignParent();
	}

	void spawnPattern()
	{
		Debug.Log("Current phase: " + levelDatabaseScript.currentLevelPhase);
		Debug.Log("Current position in phase: " + positionInPhase);
		spawnEnemy(levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase, positionInPhase]);
	}

	void setStartingPoint()
	{
		if(levelDatabaseScript.currentLevelPhase == 4)
		{
			positionInPhase = 1;
			startPosition = GameObject.Find("BossSpawnPoint").transform.position;
			spawnPattern();
			return;
		}
		for(positionInPhase = 1; positionInPhase < phaseTotal; positionInPhase++)
		{
			if(positionInPhase <= phaseTotal/2)
			{
				startPosition = this.transform.position;
			}
			else
			{
				startPosition = GameObject.Find("EnemySpawnPoint2").transform.position;
			}
			spawnPattern();
		}
	}

	void moveToNextPhase()
	{
		endPosAdjustment = 0;
		if(levelDatabaseScript.currentLevelPhase < 4)
		{
			levelDatabaseScript.currentLevelPhase++;
			Debug.Log ("Loading phase: " + levelDatabaseScript.currentLevelPhase);
			phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase, 0];
		}
		else
		{
			Debug.Log("currentLevelPhase is currently at " + levelDatabaseScript.currentLevelPhase + " and the lenght of the array is " + levelDatabaseScript.levelArray.Length);
		}
		
		newPhaseTimer = newPhaseTimerStore;
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase>();
        spawnPoint1 = GameObject.Find("EnemySpawnPoint1");
        spawnPoint2 = GameObject.Find("EnemySpawnPoint2");
        bossSpawnPoint = GameObject.Find("BossSpawnPoint");
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
