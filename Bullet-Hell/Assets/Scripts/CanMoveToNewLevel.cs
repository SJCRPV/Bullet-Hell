using UnityEngine;
using System.Collections;

public class CanMoveToNewLevel : MonoBehaviour {

	private bool detectedBoss;
	private bool bossIsAlive;
	public float timeUntilNextLevel;
	private int currentLevel;

	void loadNextLevel()
	{
		currentLevel++;
		if (timeUntilNextLevel <= 0) 
		{
			Application.LoadLevel(currentLevel);
		}
	}

	void canMoveToNextLevel()
	{
		if (GameObject.Find ("Boss1") == true) 
		{
			detectedBoss = true;
			bossIsAlive = true;
		} 
		else 
		{
			detectedBoss = false;
		}
	}

	// Use this for initialization
	void Start () {
		detectedBoss = false;
		bossIsAlive = false;
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
		canMoveToNextLevel ();
		if (detectedBoss == false && bossIsAlive == true) 
		{
			timeUntilNextLevel -= Time.deltaTime;
			loadNextLevel();
		}

	}
}
