using UnityEngine;
using System.Collections;

public enum enemyList
{
    //Change the names when you figure out what to call them
    Basic = 0,
    Cone = 1,
    Graze = 2,
    Enemy4,
    Enemy5,
    Enemy6,
    Enemy7,
    Enemy8,
    MiniBoss1 = 8,
    MiniBoss2,
    MiniBoss3,
    MiniBoss4,
    MiniBoss5,
    MiniBoss6,
    MiniBoss7,
    MiniBoss8,
    Boss1 = 16,
    Boss2,
    Boss3,
    Boss4,
    Boss5,
    Boss6,
    Boss7,
    Boss8,
}

public class GameDatabase : MonoBehaviour {

	public GameObject enemyBasic;
	public GameObject enemyCone;
	public GameObject enemyGraze;
	public GameObject enemyMiniBoss1;
	public GameObject enemyBoss1;
    // TODO: Assess if you need levelBase
    public Level levelBase;

    [SerializeField]
	private int currentLevel;
    [SerializeField]
	private int currentLevelPhase;

    public int getCurrentLevel()
    {
        return currentLevel;
    }
    public int getCurrentLevelPhase()
    {
        return currentLevelPhase;
    }

    public void incrementCurrentLevelPhase()
    {
        currentLevelPhase++;
    }

	public void Level0()
	{
		currentLevel = 0;
		Debug.Log ("You loaded level 0!");
	}
    public void Level1()
    {
        Debug.Log("You loaded level 1!");
        currentLevel = 1;
    }
    public void Level2()
    {
        Debug.Log("You loaded level 2!");
        currentLevel = 2;
        /*levelArray[0] = 5;
		levelArray[1] = 18;
		levelArray[2] = 18;
		levelArray[3] = 14;
		levelArray[4] = 1;
		levelArray[5] = 18;
		levelArray[6] = 20;
		levelArray[7] = 24;
		levelArray[8] = 28;
		levelArray[9] = 1;*/
    }

    void OnLevelWasLoaded(int level)
	{
		switch(level)
		{
		case 0:
			Level0();
			break;
			
		case 1:
			Level1();
			break;
			
		case 2:
			Level2();
			break;
			
		default:
			break;
		}
	}

	// Use this for initialization
	void Start () {
        levelBase = GetComponent<Level>();
        currentLevel = levelBase.getLevelNum();
		currentLevelPhase = -1;
	}
}
