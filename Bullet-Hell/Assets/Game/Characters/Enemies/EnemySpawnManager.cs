using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    private GameObject[] bossPoints;
    private GameObject enemyInstance;
    private GameObject currentSpawnPoint1;
    private GameObject currentSpawnPoint2;
    private Sorter sorter;
    private int currentLevel;
    private int phaseTotal;
    private int positionInPhase;
    private int phaseToSpawnCycles;
    private int spawnLengthCyclesRun = 0;
    private int phaseToEndCycles;
    private int endLengthCyclesRun = 0;
    private int selectGOinArrayFormula;
    private float newPhaseTimerStore;
    private float inbetweenSpawnTimerStore;

    int accountForBossPhases()
    {
        if (gameDatabaseScript.getCurrentLevelPhase() > levelScript.getBossPhase())
        {
            return -4;
        }
        else if (gameDatabaseScript.getCurrentLevelPhase() > levelScript.getMiniBossPhase())
        {
            return -2;
        }
        return 0;
    }

    int adjustPhaseValue(int phase)
    {
        if (phase > levelScript.getBossPhase())
        {
            phase -= 2;
        }
        else if (phase > levelScript.getMiniBossPhase())
        {
            phase--;
        }
        return phase;
    }

    void determineRatios()
    {
        Debug.Log("Number of phases in this level = " + levelScript.getNumberOfPhases());
        phaseToSpawnCycles = levelScript.getNumberOfPhases() / spawnPoints.Length;
        Debug.Log("phaseToSpawnCycles = " + phaseToSpawnCycles);
        phaseToEndCycles = levelScript.getNumberOfPhases() / endPoints.Length;
        Debug.Log("phaseToEndCycles = " + phaseToEndCycles);
    }

    GameObject[] determineStartingPoint(ref int cyclesRun, String whichPoints)
    {
        //BUG: phase 8 goes OOB for the spawnPoints
        //CLEANING: See if you can find a more intuitive way of knowing which array you're talking about without increasing the number of passed variables
        GameObject[] points;
        GameObject[] tempPoints = new GameObject[2];
        int cycles;
        int remainder = adjustPhaseValue(levelScript.getNumberOfPhases() - gameDatabaseScript.getCurrentLevelPhase());
        int phase = adjustPhaseValue(gameDatabaseScript.getCurrentLevelPhase());
        int selectGOinArrayFormulaAdjusted = -1;

        if (whichPoints.Equals("spawn", StringComparison.OrdinalIgnoreCase))
        {
            points = spawnPoints;
            cycles = phaseToSpawnCycles - cyclesRun;
        }
        else if(whichPoints.Equals("end", StringComparison.OrdinalIgnoreCase))
        {
            points = endPoints;
            cycles = phaseToEndCycles - cyclesRun;
        }
        else
        {
            Debug.LogError("Invalid String argument. No such GameObject array corresponding to that String. Returning null array");
            return new GameObject[2];
        }

        //CLEANING: You probably don't need this bool nor the checks it makes.
        bool isAboveLength = false;

        if (selectGOinArrayFormula >= points.Length)
        {
            isAboveLength = true;
        }

        if (selectGOinArrayFormula + accountForBossPhases() - (cyclesRun * points.Length) >= points.Length)
        {
            cyclesRun++;
            Debug.Log("cyclesRun incremented! Now at: " + cyclesRun);
        }

        selectGOinArrayFormulaAdjusted = selectGOinArrayFormula + accountForBossPhases() - (cyclesRun * points.Length);
        Debug.Log("Set selectGOinArrayFormulaAdjusted as: " + selectGOinArrayFormulaAdjusted);
        if (cyclesRun <= cycles)
        {
            if(isAboveLength)
            {
                tempPoints[0] = points[selectGOinArrayFormulaAdjusted];
                tempPoints[1] = points[selectGOinArrayFormulaAdjusted + 1];
            }
            else
            {
                tempPoints[0] = points[selectGOinArrayFormula];
                tempPoints[1] = points[selectGOinArrayFormula + 1];
            }
        }
        else
        {
            Debug.Log("Remainder is at: " + remainder);
            if (remainder > 0)
            {
                tempPoints[0] = points[selectGOinArrayFormulaAdjusted];
                tempPoints[1] = points[selectGOinArrayFormulaAdjusted + 1];
                if (positionInPhase == levelScript.getPhaseLenght(gameDatabaseScript.getCurrentLevelPhase()) - 1)
                {
                    remainder--;
                }
            }
            else
            {
                Debug.LogError("The remainder is 0. I don't know what points to assign!");
            }
        }

        return tempPoints;
    }

    void setStartingPoint(GameObject spawnedEnemy)
    {
        GameObject tempSpawnPoint1 = null;
        GameObject tempSpawnPoint2 = null;
        GameObject tempEndPoint1 = null;
        GameObject tempEndPoint2 = null;

        int currentPhase = gameDatabaseScript.getCurrentLevelPhase();

        if (currentPhase == levelScript.getMiniBossPhase() || currentPhase == levelScript.getBossPhase())
        {
            spawnedEnemy.GetComponent<Movement_Generic>().spawnPoint = bossPoints[0];
            spawnedEnemy.GetComponent<Movement_Generic>().leavePoint = bossPoints[1];

            currentSpawnPoint1 = bossPoints[0];
            currentSpawnPoint2 = bossPoints[0];
            return;
        }
        else
        {
            if(phaseToSpawnCycles >= 0)
            {
                Debug.Log("Spawn");
                GameObject[] temp = determineStartingPoint(ref spawnLengthCyclesRun, "Spawn");
                tempSpawnPoint1 = temp[0];
                tempSpawnPoint2 = temp[1];
            }

            if(phaseToEndCycles >= 0)
            {
                Debug.Log("End");
                GameObject[] temp = determineStartingPoint(ref endLengthCyclesRun, "end");
                tempEndPoint1 = temp[0];
                tempEndPoint2 = temp[1];
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
        //BUG: Spawning starts at (0,0) coordinates
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
            Debug.Log ("Loading phase: " + gameDatabaseScript.getCurrentLevelPhase());
            phaseTotal = levelScript.getPhaseLenght(gameDatabaseScript.getCurrentLevelPhase());
            selectGOinArrayFormula = 2 * gameDatabaseScript.getCurrentLevelPhase();
            Debug.Log("selectGOinArrayFormula: " + selectGOinArrayFormula);
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
        Debug.Log("spawnPoints.Length: " + spawnPoints.Length);
        endPoints = GameObject.FindGameObjectsWithTag("EndPoint");
        Debug.Log("endPoints.Length: " + endPoints.Length);
        bossPoints = GameObject.FindGameObjectsWithTag("BossPoint");
        System.Array.Sort(spawnPoints, sorter);
        System.Array.Sort(endPoints, sorter);
        System.Array.Sort(bossPoints, sorter);
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
