using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

    [HideInInspector]
    public LevelDatabase levelDatabaseScript;
    [HideInInspector]
    public Movement movementScript;
    public float newPhaseTimer;
    public float inbetweenSpawnTimer;

    private GameObject enemyInstance;
    private int currentLevel;
    private int phaseTotal;
    private int positionInPhase;
    private float newPhaseTimerStore;
    private float inbetweenSpawnTimerStore;

    void assignParent()
    {
        enemyInstance.transform.parent = enemyInstance.GetComponent<Movement>().spawnPoint.transform;
    }

    void spawnEnemy(int objectToSpawn)
    {
        switch (objectToSpawn)
        {
            case 0:
                //Debug.Log("Spawned a basic!");
                enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBasic, levelDatabaseScript.enemyBasic.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
                enemyInstance.name = "Basic";
                break;

            case 1:
                //Debug.Log ("Spawned a cone!");
                enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyCone, levelDatabaseScript.enemyCone.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
                enemyInstance.name = "Cone";
                break;

            case 2:
                //Debug.Log ("Spawned a graze!");
                enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyGraze, levelDatabaseScript.enemyGraze.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
                enemyInstance.name = "Graze";
                break;

            case 8:
                Debug.Log("Spawned a miniBoss1!");
                enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyMiniBoss1, levelDatabaseScript.enemyMiniBoss1.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
                enemyInstance.name = "MiniBoss1";
                break;

            case 16:
                Debug.Log("Spawned boss1");
                enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyBoss1, levelDatabaseScript.enemyBoss1.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
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
        Debug.Log("Current phase: " + levelDatabaseScript.currentLevelPhase + "\nPhase total: " + phaseTotal);
        Debug.Log("Current position in phase: " + positionInPhase);
        if(positionInPhase > phaseTotal / 2 && movementScript.getOffset() > phaseTotal / 4)
        {
            movementScript.resetOffset();
        }
        spawnEnemy(levelDatabaseScript.levelArray[levelDatabaseScript.currentLevelPhase][positionInPhase]);
    }

    void setStartingPoint()
    {
        //This may not be future proof depending on how you design the levels. It implies you won't have more spawn points than these per level.
        GameObject tempSpawnPoint1 = null;
        GameObject tempSpawnPoint2 = null;
        GameObject tempLeavePoint1 = null;
        GameObject tempLeavePoint2 = null;
        switch(levelDatabaseScript.currentLevelPhase)
        {
            case 0:
                tempSpawnPoint1 = GameObject.Find("AEnemySpawnPoint1");
                tempSpawnPoint2 = GameObject.Find("EnemySpawnPoint2");
                tempLeavePoint1 = GameObject.Find("EnemyEndPoint1/3");
                tempLeavePoint2 = GameObject.Find("EnemyEndPoint2/4");
                break;
            case 1:
                tempSpawnPoint1 = GameObject.Find("EnemySpawnPoint3");
                tempSpawnPoint2 = GameObject.Find("EnemySpawnPoint4");
                tempLeavePoint1 = GameObject.Find("EnemyEndPoint1/3");
                tempLeavePoint2 = GameObject.Find("EnemyEndPoint2/4");
                break;
            case 2:
                tempSpawnPoint1 = GameObject.Find("EnemySpawnPoint5");
                tempSpawnPoint2 = GameObject.Find("EnemySpawnPoint6");
                tempLeavePoint1 = GameObject.Find("EnemyEndPoint6");
                tempLeavePoint2 = GameObject.Find("EnemyEndPoint5");
                break;
            case 3:
                tempSpawnPoint1 = GameObject.Find("EnemySpawnPoint7");
                tempSpawnPoint2 = GameObject.Find("EnemySpawnPoint8");
                tempLeavePoint1 = GameObject.Find("EnemyEndPoint7");
                tempLeavePoint2 = GameObject.Find("EnemyEndPoint8");
                break;
            case 4:
                tempSpawnPoint1 = GameObject.Find("ABossSpawnPoint");
                tempLeavePoint1 = GameObject.Find("ABossLeavePoint");
                break;
            case 5:
                tempSpawnPoint1 = GameObject.Find("EnemySpawnPoint7");
                tempSpawnPoint2 = GameObject.Find("EnemySpawnPoint8");
                tempLeavePoint1 = GameObject.Find("EnemyEndPoint7");
                tempLeavePoint2 = GameObject.Find("EnemyEndPoint8");
                break;
            case 6:
                tempSpawnPoint1 = GameObject.Find("AEnemySpawnPoint1");
                tempSpawnPoint2 = GameObject.Find("EnemySpawnPoint2");
                tempLeavePoint1 = GameObject.Find("EnemyEndPoint1/3");
                tempLeavePoint2 = GameObject.Find("EnemyEndPoint2/4");
                break;
            case 7:
                tempSpawnPoint1 = GameObject.Find("EnemySpawnPoint3");
                tempSpawnPoint2 = GameObject.Find("EnemySpawnPoint4");
                tempLeavePoint1 = GameObject.Find("EnemyEndPoint1/3");
                tempLeavePoint2 = GameObject.Find("EnemyEndPoint2/4");
                break;
            case 8:
                tempSpawnPoint1 = GameObject.Find("EnemySpawnPoint5");
                tempSpawnPoint2 = GameObject.Find("EnemySpawnPoint6");
                tempLeavePoint1 = GameObject.Find("EnemyEndPoint6");
                tempLeavePoint2 = GameObject.Find("EnemyEndPoint5");
                break;
            case 9:
                tempSpawnPoint1 = GameObject.Find("ABossSpawnPoint");
                tempLeavePoint1 = GameObject.Find("ABossLeavePoint");
                break;
        }
        if (levelDatabaseScript.currentLevelPhase == 4)
        {
            levelDatabaseScript.enemyMiniBoss1.GetComponent<Movement>().spawnPoint = tempSpawnPoint1;
            levelDatabaseScript.enemyMiniBoss1.GetComponent<Movement>().leavePoint = tempLeavePoint1;
            return;
        }
        if (positionInPhase < phaseTotal / 2)
        {
            levelDatabaseScript.enemyBasic.GetComponent<Movement>().spawnPoint = tempSpawnPoint1;
            levelDatabaseScript.enemyBasic.GetComponent<Movement>().leavePoint = tempLeavePoint1;
            levelDatabaseScript.enemyCone.GetComponent<Movement>().spawnPoint = tempSpawnPoint1;
            levelDatabaseScript.enemyCone.GetComponent<Movement>().leavePoint = tempLeavePoint1;
            levelDatabaseScript.enemyGraze.GetComponent<Movement>().spawnPoint = tempSpawnPoint1;
            levelDatabaseScript.enemyGraze.GetComponent<Movement>().leavePoint = tempLeavePoint1;
        }
        else
        {
            levelDatabaseScript.enemyBasic.GetComponent<Movement>().spawnPoint = tempSpawnPoint2;
            levelDatabaseScript.enemyBasic.GetComponent<Movement>().leavePoint = tempLeavePoint2;
            levelDatabaseScript.enemyCone.GetComponent<Movement>().spawnPoint = tempSpawnPoint2;
            levelDatabaseScript.enemyCone.GetComponent<Movement>().leavePoint = tempLeavePoint2;
            levelDatabaseScript.enemyGraze.GetComponent<Movement>().spawnPoint = tempSpawnPoint2;
            levelDatabaseScript.enemyGraze.GetComponent<Movement>().leavePoint = tempLeavePoint2;
        }
    }

    private void moveToNextPhase()
    {
        if (levelDatabaseScript.currentLevelPhase < 10)
        {
            levelDatabaseScript.currentLevelPhase++;
            movementScript.resetOffset();
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
        movementScript = levelDatabaseScript.enemyBasic.GetComponent<Movement>();
        newPhaseTimerStore = newPhaseTimer;
        inbetweenSpawnTimerStore = inbetweenSpawnTimer;
        positionInPhase = 0;
	}
	
	// Update is called once per frame
	void Update () {
        newPhaseTimer -= Time.deltaTime;
        if (newPhaseTimer <= 0)
        {
            moveToNextPhase();
            positionInPhase = 0;
        }
        inbetweenSpawnTimer -= Time.deltaTime;
        if (positionInPhase < phaseTotal)
        {
            if (inbetweenSpawnTimer <= 0)
            {
                setStartingPoint();
                spawnPattern();
                positionInPhase++;
                inbetweenSpawnTimer = inbetweenSpawnTimerStore;
            }
        }
	}
}
