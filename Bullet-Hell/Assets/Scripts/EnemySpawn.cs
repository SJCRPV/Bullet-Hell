using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	GameObject enemyInstance;
	
	static LevelDatabase levelInfoScript;

	private Vector3 startPosition;
	private bool isLerping = true;
	private float timeLerpStart;
	private Vector3 endVector;
	private Vector3 addOn;
	private int looper;
	
	public float speed = 1f;
	public int health = 3;
	public float lerpTime = 1.0f;
	public float percentageComplete;
	public GameObject enemyPrefab;

	public bool isItLerping()
	{
		if(isLerping != null)
		{
			return isLerping;
		}
	}
	
	void StartLerping () {
		isLerping = true;
		percentageComplete = 0;
		timeLerpStart = Time.time;
		startPosition = transform.position;
		endVector = GameObject.Find ("EnemyEndPoint1").transform.position;
	}
	
	// Use this for initialization
	void Start () {
		levelInfoScript = GetComponent<LevelDatabase> ();
		SpawnEnemy();
	}

	Vector3 addOnDeterminer(){
		looper++;
		if (looper > levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]) 
		{
			looper = 0;
		}
		addOn = new Vector3 (0.7f * looper, 0, 0);
		
		return addOn;
	}
	
	void ChangePosition()
	{
		if(isLerping)
		{
			percentageComplete = timeLerpStart/lerpTime;
			enemyInstance.transform.position = Vector3.Lerp(startPosition, endVector + addOnDeterminer(), percentageComplete);
			
			if(percentageComplete >= lerpTime)
			{
				isLerping = false;
			}
		}
	}

	void SpawnEnemy()
	{

		levelInfoScript.Level1 ();
	
		//What you want is for the loop to go through each of the phase's lenght
		for (int i = 0; i < levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]; i++) 
		{
			if (i == levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]) 
			{
				levelInfoScript.currentLevelPhase++;
			} 
			else 
			{
				//Apparently, the engine doesn't know what to do after moving the first one. Check again to
				//see if arrays REALLY don't work in this situation.
				enemyInstance = (GameObject)Instantiate (enemyPrefab, transform.position, Quaternion.identity);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		StartLerping();
		ChangePosition();
	}


}
