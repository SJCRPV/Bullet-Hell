using UnityEngine;
using System.Collections;

public class LevelDatabase : MonoBehaviour {

	public int currentLevel;
	public int currentLevelPhase;

	public int[] levelArray = new int[5];

	public void Level0()
	{
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
	}

	// Use this for initialization
	void Start () {
		currentLevel = 0;
		currentLevelPhase = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
