using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	GameObject[] enemyInstance = new GameObject[50];
	static LevelDatabase levelInfoScript;
	//static EnemyMove enemyMoveScript;
	
	private int totalEnemiesInLevel;
	//Holds the value of the array at the index of the current phase
	public int phaseTotal;
	//Holds the value of all enemies that can exist until the current phase
	public int currentTotal;
	public Vector3 startPosition;
	public float speed = 1f;
	//Timer that ticks down until you run SpawnEnemy() again.
	public float newSpawnTimer = 5.0f;
	//Stores the value of newSpawnTimer for later use
	float storeSpawnTimer;
	public GameObject enemyPrefab;

	//All the flags!
	public bool canSetEndPositions = false;
	public bool canResetPercentage = false;

	public int getTotalEnemiesInLevel()
	{
		return totalEnemiesInLevel;
	}

	void whichLevel()
	{
		switch(levelInfoScript.currentLevel)
		{
		case 1:
			levelInfoScript.Level1 ();
			phaseTotal = levelInfoScript.levelArray[levelInfoScript.currentLevelPhase];
			break;
		default:
		Debug.Log("Error!");
		Debug.Log(levelInfoScript.levelArray.Length);
		Debug.Log(levelInfoScript.currentLevel);
		Debug.Log(levelInfoScript.currentLevelPhase);
		break;
		}
	}

	void SpawnEnemy()
	{
		whichLevel ();
		currentTotal += levelInfoScript.levelArray[levelInfoScript.currentLevelPhase];
	
		//What you want is for the loop to go through each of the phase's lenght
		for (int i = 0; i < phaseTotal; i++) 
		{
			//This creates an enemy!
			enemyInstance[i] = (GameObject)Instantiate (enemyPrefab, transform.position, Quaternion.identity);
		}
	}

	void setTotalEnemies()
	{
		totalEnemiesInLevel = 0;
		for(int i = 0; i < levelInfoScript.levelArray.Length; i++)
		{
			Debug.Log(totalEnemiesInLevel += levelInfoScript.levelArray[i]);
		}
	}

	void newPhase()
	{
		//If you've gone through all the level's phases.
		if(levelInfoScript.currentLevelPhase > levelInfoScript.levelArray.Length)
		{
			levelInfoScript.currentLevel++;
			levelInfoScript.currentLevelPhase = 0;
			currentTotal = levelInfoScript.levelArray[0];

			//enemyMoveScript.resetPercentage();
			canResetPercentage = true;

			Debug.Log("New level!");
			Debug.Log (phaseTotal = levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]);
			setTotalEnemies();
		}
		else
		{
			Debug.Log("New Phase!");
			levelInfoScript.currentLevelPhase++;
			Debug.Log(phaseTotal = levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]);
			startPosition = GameObject.Find("EnemySpawnPoint2").transform.position;

			//enemyMoveScript.resetPercentage();
			canResetPercentage = true;

			newSpawnTimer = storeSpawnTimer;
		}
	}

	// Use this for initialization
	void Start () {
		levelInfoScript = GetComponent<LevelDatabase> ();
		storeSpawnTimer = newSpawnTimer;
		//enemyMoveScript = GetComponent<EnemyMove> ();

		//This is temporary, remove after creating the menu!
		levelInfoScript.currentLevel = 1;

		startPosition = transform.position;
		SpawnEnemy();
		setTotalEnemies();
	}

	// Update is called once per frame
	void Update () {
		//Run spawn enemy again after a certain time!
		newSpawnTimer -= Time.deltaTime;
		if(newSpawnTimer <= 0)
		{
			canSetEndPositions = true;
			newPhase();
			SpawnEnemy();
		}
	}
}
