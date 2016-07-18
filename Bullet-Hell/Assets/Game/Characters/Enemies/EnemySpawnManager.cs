using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sorter : IComparer
{

    // Calls CaseInsensitiveComparer.Compare on the monster name string.
    int IComparer.Compare(System.Object x, System.Object y)
    {
        return ((new CaseInsensitiveComparer()).Compare(((GameObject)x).name, ((GameObject)y).name));
    }

}

public class EnemySpawnManager : MonoBehaviour {

    [HideInInspector]
    public GameDatabase gameDatabaseScript;
    [HideInInspector]
    public Movement_Generic movementScript;
    [HideInInspector]
    public Level levelScript;
    public float newPhaseTimer;
    public float inbetweenSpawnTimer;

    private GameObject[] spawnPoints;
    private GameObject[] endPoints;
    private GameObject enemyInstance;
    private GameObject currentSpawnPoint1;
    private GameObject currentSpawnPoint2;
    private Sorter sorter;
    private int currentLevel;
    private int phaseTotal;
    private int positionInPhase;
    private int cycles;
    private int remainder;
    private float newPhaseTimerStore;
    private float inbetweenSpawnTimerStore;

    void determineRatios()
    {
        cycles = spawnPoints.Length / endPoints.Length;
        remainder = spawnPoints.Length % endPoints.Length;
    }

    void setStartingPoint(GameObject spawnedEnemy)
    {
        //BUG: There are more phases than spawn points, so 1 + 2 * phase still needs to check if it goes beyond the spawnPoints array.

        GameObject tempSpawnPoint1 = null;
        GameObject tempSpawnPoint2 = null;
        GameObject tempEndPoint1 = null;
        GameObject tempEndPoint2 = null;

        int phase = gameDatabaseScript.getCurrentLevelPhase();

        if (phase == 4 || phase == 9)
        {
            spawnedEnemy.GetComponent<Movement_Generic>().spawnPoint = spawnPoints[0];
            spawnedEnemy.GetComponent<Movement_Generic>().leavePoint = endPoints[0];

            currentSpawnPoint1 = spawnPoints[0];
            currentSpawnPoint2 = spawnPoints[0];
            return;
        }
        else
        {
            tempSpawnPoint1 = spawnPoints[1 + 2 * phase];
            tempSpawnPoint2 = spawnPoints[2 + 2 * phase];


            if (cycles > 0)
            {
                if (2 + 2 * phase >= endPoints.Length)
                {
                    cycles--;
                    int temp = 2 + 2 * phase;
                    while (temp >= endPoints.Length)
                    {
                        temp = temp - endPoints.Length;
                    }
                    tempEndPoint1 = endPoints[temp - 1];
                    tempEndPoint1 = endPoints[temp];
                }
                else
                {
                    tempEndPoint1 = endPoints[1 + 2 * phase];
                    tempEndPoint2 = endPoints[2 + 2 * phase];
                }
            }
            else
            {
                if (remainder > 0)
                {
                    int i = remainder - 1;
                    tempEndPoint1 = endPoints[1 + 2 * remainder - i];
                    tempEndPoint2 = endPoints[2 + 2 * remainder - i];
                    i--;
                }
            }

        }
        if (positionInPhase < phaseTotal / 2)
        {
            spawnedEnemy.GetComponent<Movement_Generic>().spawnPoint = tempSpawnPoint1;
            spawnedEnemy.GetComponent<Movement_Generic>().leavePoint = tempEndPoint1;
        }
        else
        {
            spawnedEnemy.GetComponent<Movement_Generic>().spawnPoint = tempSpawnPoint2;
            spawnedEnemy.GetComponent<Movement_Generic>().leavePoint = tempEndPoint2;
        }

        currentSpawnPoint1 = tempSpawnPoint1;
        currentSpawnPoint2 = tempSpawnPoint2;
    }

    void assignParent()
    {
        enemyInstance.transform.parent = enemyInstance.GetComponent<Movement_Generic>().spawnPoint.transform;
    }

    void spawnEnemy(int objectToSpawn)
    {
        switch (objectToSpawn)
        {
            case 0:
                //Debug.Log("Spawned a basic!");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyBasic);
                enemyInstance.name = "Basic";
                break;

            case 1:
                //Debug.Log ("Spawned a cone!");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyCone);
                enemyInstance.name = "Cone";
                break;

            case 2:
                //Debug.Log ("Spawned a graze!");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyGraze);
                enemyInstance.name = "Graze";
                break;

            case 8:
                //Debug.Log("Spawned a miniBoss1!");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyMiniBoss1);
                enemyInstance.name = "MiniBoss1";
                break;

            case 16:
                //Debug.Log("Spawned boss1");
                enemyInstance = (GameObject)Instantiate(gameDatabaseScript.enemyBoss1);
                enemyInstance.name = "Boss1";
                break;

            default:
                Debug.LogError("I did not spawn anything with the number " + objectToSpawn);
                break;
        }
        setStartingPoint(enemyInstance);
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
        //Debug.Log("getSpecificContentAtIndex " + positionInPhase + " in phase " + gameDatabaseScript.getCurrentLevelPhase() + " is " + levelScript.getSpecificContentAtIndex(gameDatabaseScript.getCurrentLevelPhase(), positionInPhase));
        spawnEnemy(levelScript.getSpecificContentAtIndex(gameDatabaseScript.getCurrentLevelPhase(), positionInPhase));
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
        sorter = new Sorter();
        gameDatabaseScript = GetComponent<GameDatabase>();
        movementScript = gameDatabaseScript.enemyBasic.GetComponent<Movement_Generic>();
        levelScript = GetComponent<Level>();
        newPhaseTimerStore = newPhaseTimer;
        inbetweenSpawnTimerStore = inbetweenSpawnTimer;
        positionInPhase = 0;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        endPoints = GameObject.FindGameObjectsWithTag("EndPoint");
        System.Array.Sort(spawnPoints, sorter);
        System.Array.Sort(endPoints, sorter);
        determineRatios();
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
                //Debug.Log("Current phase: " + gameDatabaseScript.getCurrentLevelPhase());
                //Debug.Log("currentSpawnPoint1 child count: " + currentSpawnPoint1.transform.childCount);
                //Debug.Log("currentSpawnPoint2 child count: " + currentSpawnPoint2.transform.childCount);
                if (currentSpawnPoint1.transform.childCount == 0 && currentSpawnPoint2.transform.childCount == 0)
                {
                    newPhaseTimer -= Time.deltaTime;
                }
                break;

            default:
                newPhaseTimer -= Time.deltaTime;
                break;
        }
        if (newPhaseTimer <= 0 && gameDatabaseScript.getCurrentLevelPhase() != 9)
        {
            moveToNextPhase();
            positionInPhase = 0;
        }
        else if (newPhaseTimer <= 0 && gameDatabaseScript.getCurrentLevelPhase() == 9)
        {
            GameObject.Find("Main Camera").SendMessage("loadNextLevel");
        }

        inbetweenSpawnTimer -= Time.deltaTime;

        if (positionInPhase < phaseTotal)
        {
            if (inbetweenSpawnTimer <= 0)
            {
                //setStartingPoint();
                spawnPattern();
                positionInPhase++;
                inbetweenSpawnTimer = inbetweenSpawnTimerStore;
            }
        }
	}
}
