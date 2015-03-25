using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	GameObject[] enemyInstance = new GameObject[50];
	
	static LevelDatabase levelInfoScript;

	private Vector3 startPosition;
	private bool[] isLerping = new bool[50];
	private bool[] stopLerping = new bool[50];
	private float[] timeLerpStart = new float[50];
	private Vector3[] endVector = new Vector3[50];
	private float[] percentageComplete = new float[50];
	private Vector3 addOn;
	private int looper;
	
	public float speed = 1f;
	public int health = 3;
	public float lerpTime = 1.0f;
	public GameObject enemyPrefab;

	public bool isItLerping(int i)
	{
		if(isLerping[i] != null)
		{
			return isLerping[i];
		}
	}
	
	void StartLerping () {
		//Do the loops for these guys
		for (int i = 0; i < levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]; i++) {
			isLerping [i] = true;
			percentageComplete [i] = 0f;
			timeLerpStart [i] = Time.time;
			startPosition = transform.position;
			//if (i < levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]) {
				endVector [i] = GameObject.Find ("EnemyEndPoint1").transform.position + addOnDeterminer ();
			//}
			/*else
			{
				endVector [i] = GameObject.Find ("EnemyEndPoint1").transform.position + addOnDeterminer ();
			}*/
		}
	}
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < isLerping.Length; i++) {
			isLerping[i] = false;
			stopLerping [i] = false;
		}

		levelInfoScript = GetComponent<LevelDatabase> ();
		SpawnEnemy();
	}

	Vector3 addOnDeterminer(){
		looper++;
		if (looper > levelInfoScript.levelArray[levelInfoScript.currentLevelPhase] - 1) 
		{
			looper = 0;
		}
		addOn = new Vector3 (0.8f * looper, 0, 0);
		
		return addOn;
	}
	
	void ChangePosition()
	{
		for(int i = 0; i < levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]; i++)
		{
			if (isLerping[i] && stopLerping[i] == false)
			{
				percentageComplete[i] = timeLerpStart[i]/lerpTime;
				enemyInstance[i].transform.position = Vector3.Lerp(startPosition, endVector[i], percentageComplete[i]);

				if(percentageComplete[i] >= lerpTime)
				{
					isLerping[i] = false;
					stopLerping[i] = true;
				}
			}
		}
	}

	void SpawnEnemy()
	{

		levelInfoScript.Level1 ();
	
		//What you want is for the loop to go through each of the phase's lenght
		for (int i = 0; i < levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]; i++) 
		{
			if(levelInfoScript.currentLevelPhase >= levelInfoScript.levelArray.Length)
			{
				break;
			}
			if (i == levelInfoScript.levelArray[levelInfoScript.currentLevelPhase]) 
			{
				levelInfoScript.currentLevelPhase++;
			} 

			//This creates an enemy!
			enemyInstance[i] = (GameObject)Instantiate (enemyPrefab, transform.position, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update () {
		StartLerping();
		ChangePosition();
	}


}
