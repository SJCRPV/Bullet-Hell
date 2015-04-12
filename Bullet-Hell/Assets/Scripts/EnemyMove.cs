using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	static EnemySpawn enemySpawnScript;
	static LevelDatabase levelDatabaseScript;

	private int totalEnemiesInLevel;
	//Flag that tells you if it's currently lerping or not
	private bool[] isLerping;
	//Had as some sort of li;miter. Might be useless
	private bool[] stopLerping;
	private Vector3[] endPosition;
	//Time since the instance started lerping. Used to tell how close to the end it is
	private float[] timeLerpStart;
	private float[] percentageComplete;
	//currentTotal - phaseTotal. So you know which number to start the loops on.
	private int enemyNumberCountStart;
	//How much it's added to the end position for the ship's position
	private Vector3 addOn;
	//It's the variable that gets added to the X axis when deciding a ship's position
	private float looper = 0;
	//How long (1.0f = 1 second) should the lerp take?
	public float lerpTime = 1.0f;
	public bool percentageCompleteFlag = false;

	public void setEnemyNumberCountStart()
	{
		enemyNumberCountStart = enemySpawnScript.currentTotal - enemySpawnScript.phaseTotal;
	}

	public void resetPercentage()
	{
		for(int i = 0; i < percentageComplete.Length; i++)
		{
			percentageComplete[i] = 0;
		}
		enemySpawnScript.canResetPercentage = false;
	}

	public bool isItLerping(int i)
	{
		return isLerping[i];
	}

	Vector3 addOnDeterminer(){
		looper += 0.8f;
		if (looper >= 5) 
		{
			looper = 0;
		}
		addOn = new Vector3 (looper, 0, 0);
		
		return addOn;
	}
	
	void setLerps()
	{
		for (int i = 0; i < isLerping.Length; i++) {
			isLerping[i] = false;
			stopLerping [i] = false;
		}
	}

	void StartLerping () {
		//Setting all the info for the Lerp
		for (int i = 0; i < enemyNumberCountStart; i++) 
		{
			if(percentageComplete[i] < lerpTime)
			{
				isLerping [i] = true;
				percentageComplete [i] = 0f;
				timeLerpStart [i] = Time.time;
			}
		}
	}

	public void setEndPositions()
	{
		for(int i = 0; i < enemyNumberCountStart; i++)
		{
			if (i < (enemySpawnScript.phaseTotal)/2) {
				endPosition [i] = GameObject.Find ("EnemyEndPoint1").transform.position + addOnDeterminer ();
			}
			else
			{
				endPosition [i] = GameObject.Find ("EnemyEndPoint2").transform.position - addOnDeterminer ();
			}
		}
		enemySpawnScript.canSetEndPositions = false;
	}

	void ChangePosition()
	{
		for(int i = enemyNumberCountStart; i <= enemySpawnScript.currentTotal; i++)
		{
			Debug.Log("i is: " + i + ".");
			Debug.Log("currentTotal is: " + enemySpawnScript.currentTotal + ".");
			if (isLerping[i])
			{
				percentageComplete[i] = timeLerpStart[i]/lerpTime;
				this.transform.position = Vector3.Lerp(enemySpawnScript.startPosition, endPosition[i], percentageComplete[i]);
				
				if(percentageComplete[i] >= lerpTime)
				{
					isLerping[i] = false;
					stopLerping[i] = true;
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		enemySpawnScript = GetComponent<EnemySpawn> ();
		totalEnemiesInLevel = enemySpawnScript.getTotalEnemiesInLevel();
		isLerping = new bool[totalEnemiesInLevel];
		stopLerping = new bool[totalEnemiesInLevel];
		endPosition = new Vector3[totalEnemiesInLevel];
		timeLerpStart = new float[totalEnemiesInLevel];
		percentageComplete = new float[totalEnemiesInLevel];
		setLerps ();
		setEndPositions();
	}
	
	// Update is called once per frame
	void Update () {
		if(percentageCompleteFlag)
		{
			resetPercentage();
		}
		if(enemySpawnScript.canSetEndPositions)
		{
			setEndPositions();
		}
		StartLerping();
		setEndPositions();
		ChangePosition();
	}
}
