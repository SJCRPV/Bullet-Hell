using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	[HideInInspector]
	public LevelDatabase levelDatabaseScript;
	public float newPhaseTimer;
	public float inbetweenSpawnTimer;
	public Vector3 startPosition;
	
	private GameObject enemyInstance;
    private GameObject spawnPoint1;
    private GameObject spawnPoint2;
	private GameObject spawnPoint3;
	private GameObject spawnPoint4;
	private GameObject spawnPoint5;
	private GameObject spawnPoint6;
	private GameObject spawnPoint7;
	private GameObject spawnPoint8;
    private GameObject bossSpawnPoint;
	private int currentLevel;
	private int phaseTotal;
	private int positionInPhase;
	private float newPhaseTimerStore;
	private float inbetweenSpawnTimerStore;
	
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

		case 8:
			Debug.Log ("Spawned a miniBoss1!");
			enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyMiniBoss1, startPosition, Quaternion.identity);
			enemyInstance.name = "MiniBoss1";
			break;

		case 16:
			Debug.Log("Spawned boss1");
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
		spawnEnemy(levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase][positionInPhase]);
	}

	void setStartingPoint()
	{
		if(levelDatabaseScript.currentLevelPhase == 4)
		{
			startPosition = GameObject.Find("BossSpawnPoint").transform.position;
			return;
		}
		if(positionInPhase <= phaseTotal/2)
		{
			startPosition = this.transform.position;
		}
		else
		{
			startPosition = GameObject.Find("EnemySpawnPoint2").transform.position;
		}


	}

	void moveToNextPhase()
	{
		endPosAdjustment = 0;
		if(levelDatabaseScript.currentLevelPhase < 10)
		{
			levelDatabaseScript.currentLevelPhase++;
			//Debug.Log ("Loading phase: " + levelDatabaseScript.currentLevelPhase);
			phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase].Length;
		}
		else
		{
			Debug.LogError("currentLevelPhase is currently at " + levelDatabaseScript.currentLevelPhase + " and the lenght of the array is " + levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase].Length + ". I tried to use the position: " + positionInPhase);
		}
		
		newPhaseTimer = newPhaseTimerStore;
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase>();
        spawnPoint1 = GameObject.Find("AEnemySpawnPoint1");
        spawnPoint2 = GameObject.Find("EnemySpawnPoint2");
        bossSpawnPoint = GameObject.Find("ABossSpawnPoint");
		newPhaseTimerStore = newPhaseTimer;
		inbetweenSpawnTimerStore = inbetweenSpawnTimer;
		positionInPhase = 0;
	}
	
	// Update is called once per frame
	void Update () {
		newPhaseTimer -= Time.deltaTime;
		if(newPhaseTimer <= 0)
		{
			moveToNextPhase();
		    positionInPhase = 0;
		}
		inbetweenSpawnTimer -= Time.deltaTime;
		if(positionInPhase < phaseTotal)
		{
			if(inbetweenSpawnTimer <= 0)
			{
				setStartingPoint();
				spawnPattern();
                positionInPhase++;
				inbetweenSpawnTimer = inbetweenSpawnTimerStore;
			}
		}
	}
}
