using UnityEngine;
using System.Collections;

public class LevelDatabase : MonoBehaviour {

	public int currentLevel;
	public int currentLevelPhase;

	public int[] levelArray = new int[5];


	public void Level1()
	{
		currentLevel = 1;
		levelArray[0] = 14;
		levelArray[1] = 12;
		levelArray[2] = 11;
		levelArray[3] = 8;
		levelArray[4] = 1;
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
