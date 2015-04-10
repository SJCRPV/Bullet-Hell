using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	//Flag that tells you if it's currently lerping or not
	private bool[] isLerping = new bool[50];
	//Had as some sort of limiter. Might be useless
	private bool[] stopLerping = new bool[50];
	private Vector3[] endPosition = new Vector3[50];
	//Time since the instance started lerping. Used to tell how close to the end it is
	private float[] timeLerpStart = new float[50];
	private float[] percentageComplete = new float[50];
	//Holds the value of the array at the index of the current phase
	private int phaseTotal;
	//How long (1.0f = 1 second) should the lerp take?
	public float lerpTime = 1.0f;
	private Vector3 startPosition;

	public bool isItLerping(int i)
	{
		return isLerping[i];
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
		for (int i = 0; i < phaseTotal; i++) 
		{
			if(percentageComplete[i] < lerpTime)
			{
				isLerping [i] = true;
				percentageComplete [i] = 0f;
				timeLerpStart [i] = Time.time;
			}
		}
	}

	void setEndPositions()
	{
		for(int i = 0; i < phaseTotal; i++)
		{
			if (i < phaseTotal/2) {
				endPosition [i] = GameObject.Find ("EnemyEndPoint1").transform.position + addOnDeterminer ();
			}
			else
			{
				endPosition [i] = GameObject.Find ("EnemyEndPoint2").transform.position - addOnDeterminer ();
			}
		}
	}

	void ChangePosition()
	{
		for(int i = 0; i <= phaseTotal; i++)
		{
			if (isLerping[i])
			{
				percentageComplete[i] = timeLerpStart[i]/lerpTime;
				enemyInstance[i].transform.position = Vector3.Lerp(startPosition, endPosition[i], percentageComplete[i]);
				
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
		setLerps ();
		//This is temporary, remove after creating the menu!
		startPosition = transform.position;
		setEndPositions();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
