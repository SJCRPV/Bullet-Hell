using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	LevelDatabase levelDatabaseScript;
	public GameObject enemyPrefab;
	public float newPhaseTimer;

	private GameObject enemyInstance;
	private int currentLevel;
	private int currentPhase;
	private int phaseTotal;
	private float newPhaseTimerStore;
	//Increments with enemy spawns.Resets at half-phase.Determines how much you add to endPos
	//Look for a better name
	private int adjustment;

	public Vector3 endPositionAdjustment()
	{
		Vector3 addOn;
		
		if(adjustment * 0.8f > 5)
		{
			adjustment = 0;
			addOn = new Vector3( 0.8f * adjustment, 0.8f, 0);
		}
		else
		{
			addOn = new Vector3( 0.8f * adjustment, 0, 0);
		}
		
		adjustment++;
		return addOn;
	}

	void whichLevel()
	{
		switch(currentLevel)
		{
		case 0:
			levelDatabaseScript.Level0();
			break;

		case 1:
			levelDatabaseScript.Level1();
			phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase];
			break;

		case 2:
			levelDatabaseScript.Level2();
			break;

		default:
			Debug.Log("Error!");
			Debug.Log(levelDatabaseScript.levelArray.Length);
			Debug.Log(levelDatabaseScript.currentLevel);
			Debug.Log(levelDatabaseScript.currentLevelPhase);
			break;
		}
	}

	void moveToNextPhase()
	{
		if(levelDatabaseScript.currentLevelPhase > levelDatabaseScript.levelArray.Length)
		{
			levelDatabaseScript.currentLevel++;
			levelDatabaseScript.currentLevelPhase = 0;
			Debug.Log("Level: " + levelDatabaseScript.currentLevel);
			phaseTotal = levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase];
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
		Debug.Log("Entered spawnEnemy!");
		whichLevel();
		for(int i = 0; i < phaseTotal/2; i++)
		{
			enemyInstance = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			enemyInstance.transform.parent = transform;
		}
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase>();
		//Temporary until you create the menu
		currentLevel = 1;
		newPhaseTimerStore = newPhaseTimer;
		currentPhase = 0;
		spawnEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		newPhaseTimer -= Time.deltaTime;
		if(newPhaseTimer <= 0)
		{
			moveToNextPhase();
			spawnEnemy();
		}
	}
}
