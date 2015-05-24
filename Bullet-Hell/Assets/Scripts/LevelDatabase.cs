using UnityEngine;
using System.Collections;

public class LevelDatabase : MonoBehaviour {

	public int currentLevel;
	public int currentLevelPhase;

	public GameObject enemyBasic;
	public GameObject enemyCone;
	public GameObject enemyBoss1;

	public int[] levelArray = new int[5];

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

	public void Level0()
	{
		currentLevel = 0;
		Debug.Log ("You loaded level 0!");
	}
	public void Level1()
	{
		Debug.Log("You loaded level 1!");
		currentLevel = 1;
		levelArray[0] = 8;
		levelArray[1] = 10;
		levelArray[2] = 10;
		levelArray[3] = 5;
		levelArray[4] = 1;
	}
	public void Level2()
	{
		Debug.Log("You loaded level 2!");
		currentLevel = 2;
	}

	// Use this for initialization
	void Start () {
		currentLevel = 0;
		currentLevelPhase = 0;
	}
}
