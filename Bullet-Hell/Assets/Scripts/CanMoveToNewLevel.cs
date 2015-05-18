using UnityEngine;
using System.Collections;

public class CanMoveToNewLevel : MonoBehaviour {

	private bool detectedBoss;
	private bool bossIsAlive;
	private float timeUntilNextLevel;

	void loadNextLevel()
	{
		if (timeUntilNextLevel <= 0) 
		{
			Application.LoadLevel("Level2");
		}
	}

	void canMoveToNextLevel()
	{
		if (GameObject.Find ("Boss") == true) 
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
