using UnityEngine;
using System.Collections;

public class LevelDatabase : MonoBehaviour {

	public int currentLevel;
	public int currentLevelPhase;

	public int[] levelArray = new int[5];


	public void Level1()
	{
		currentLevel = 1;
		levelArray[0] = 10;
		levelArray[1] = 12;
		levelArray[2] = 7;
		levelArray[3] = 12;
		levelArray[4] = 1;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
