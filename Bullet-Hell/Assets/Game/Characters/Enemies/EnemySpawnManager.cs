using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

    [HideInInspector]
    public GameDatabase gameDatabaseScript;
    [HideInInspector]
    public Movement movementScript;
    [HideInInspector]
    public Level levelScript;
    public float newPhaseTimer;
    public float inbetweenSpawnTimer;

    private GameObject enemyInstance;
    private GameObject currentSpawnPoint1;
    private GameObject currentSpawnPoint2;
    private int currentLevel;
    private int phaseTotal;
    private int positionInPhase;
    private float newPhaseTimerStore;
    private float inbetweenSpawnTimerStore;

    //private bool canBossStart()
    //{
    //    if(gameDatabaseScript.getCurrentLevelPhase() == 3)
    //    {
    //        if()
    //    }
    //}

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
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyBasic, gameDatabaseScript.enemyBasic.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
                enemyInstance.name = "Basic";
                break;

            case 1:
                //Debug.Log ("Spawned a cone!");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyCone, gameDatabaseScript.enemyCone.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
                enemyInstance.name = "Cone";
                break;

            case 2:
                //Debug.Log ("Spawned a graze!");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyGraze, gameDatabaseScript.enemyGraze.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
                enemyInstance.name = "Graze";
                break;

            case 8:
                Debug.Log("Spawned a miniBoss1!");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyMiniBoss1, gameDatabaseScript.enemyMiniBoss1.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
                enemyInstance.name = "MiniBoss1";
                break;

            case 16:
                Debug.Log("Spawned boss1");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyBoss1, gameDatabaseScript.enemyBoss1.GetComponent<Movement>().spawnPoint.transform.position, Quaternion.identity);
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
        //Debug.Log("Current phase: " + levelDatabaseScript.getCurrentLevelPhase() + "\nPhase total: " + phaseTotal);
        //Debug.Log("Current position in phase: " + positionInPhase);
        if(positionInPhase > phaseTotal / 2 && movementScript.getOffset() > phaseTotal / 4)
        {
            movementScript.resetOffset();
        }
        spawnEnemy(levelScript.getSpecificContentAtIndex(gameDatabaseScript.getCurrentLevelPhase(), positionInPhase));
    }

    void setStartingPoint()
    {
        //This may not be future proof depending on how you design the levels. It implies you won't have more spawn points than these per level.
        GameObject tempSpawnPoint1 = null;
        GameObject tempSpawnPoint2 = null;
        GameObject tempLeavePoint1 = null;
        GameObject tempLeavePoint2 = null;
        switch(gameDatabaseScript.getCurrentLevelPhase())
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
        if (gameDatabaseScript.getCurrentLevelPhase() == 4)
        {
            gameDatabaseScript.enemyMiniBoss1.GetComponent<Movement>().spawnPoint = tempSpawnPoint1;
            gameDatabaseScript.enemyMiniBoss1.GetComponent<Movement>().leavePoint = tempLeavePoint1;
            return;
        }
        if (positionInPhase < phaseTotal / 2)
        {
            gameDatabaseScript.enemyBasic.GetComponent<Movement>().spawnPoint = tempSpawnPoint1;
            gameDatabaseScript.enemyBasic.GetComponent<Movement>().leavePoint = tempLeavePoint1;
            gameDatabaseScript.enemyCone.GetComponent<Movement>().spawnPoint = tempSpawnPoint1;
            gameDatabaseScript.enemyCone.GetComponent<Movement>().leavePoint = tempLeavePoint1;
            gameDatabaseScript.enemyGraze.GetComponent<Movement>().spawnPoint = tempSpawnPoint1;
            gameDatabaseScript.enemyGraze.GetComponent<Movement>().leavePoint = tempLeavePoint1;
        }
        else
        {
            gameDatabaseScript.enemyBasic.GetComponent<Movement>().spawnPoint = tempSpawnPoint2;
            gameDatabaseScript.enemyBasic.GetComponent<Movement>().leavePoint = tempLeavePoint2;
            gameDatabaseScript.enemyCone.GetComponent<Movement>().spawnPoint = tempSpawnPoint2;
            gameDatabaseScript.enemyCone.GetComponent<Movement>().leavePoint = tempLeavePoint2;
            gameDatabaseScript.enemyGraze.GetComponent<Movement>().spawnPoint = tempSpawnPoint2;
            gameDatabaseScript.enemyGraze.GetComponent<Movement>().leavePoint = tempLeavePoint2;
        }

        currentSpawnPoint1 = tempSpawnPoint1;
        currentSpawnPoint2 = tempSpawnPoint2;
    }

    private void moveToNextPhase()
    {
        if (gameDatabaseScript.getCurrentLevelPhase() < 10)
        {
            gameDatabaseScript.incrementCurrentLevelPhase();
            movementScript.resetOffset();
            //Debug.Log ("Loading phase: " + levelDatabaseScript.getCurrentLevelPhase());
            phaseTotal = levelScript.getPhaseLenght(gameDatabaseScript.getCurrentLevelPhase());
        }
        else
        {
            Debug.LogError("currentLevelPhase is currently at " + gameDatabaseScript.getCurrentLevelPhase() + " and the lenght of the array is " + levelScript.getPhaseLenght(gameDatabaseScript.getCurrentLevelPhase()) + ". I tried to use the position: " + positionInPhase);
        }

        newPhaseTimer = newPhaseTimerStore;
    }

	// Use this for initialization
	void Start () {
        gameDatabaseScript = GetComponent<GameDatabase>();
        movementScript = gameDatabaseScript.enemyBasic.GetComponent<Movement>();
        levelScript = GetComponent<Level>();
        newPhaseTimerStore = newPhaseTimer;
        inbetweenSpawnTimerStore = inbetweenSpawnTimer;
        positionInPhase = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(gameDatabaseScript.getCurrentLevelPhase())
        {
            case 3:
            case 4:
            case 8:
            case 9:
                Debug.Log(gameDatabaseScript.getCurrentLevelPhase());
                Debug.Log(currentSpawnPoint1.transform.childCount);
                Debug.Log(currentSpawnPoint2.transform.childCount);
                if (currentSpawnPoint1.transform.childCount == 0 && currentSpawnPoint2.transform.childCount == 0)
                {
                    newPhaseTimer -= Time.deltaTime;
                }
                break;

            default:
                newPhaseTimer -= Time.deltaTime;
                break;
        }
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
