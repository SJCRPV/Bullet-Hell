using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	SpawnEnemies spawnEnemiesScript;
	Ballistics ballisticsScript;
	ConePattern conePatternScript;
	Boss1Ballistics boss1PatternScript;

	public float speed;
	public float timerUntilObjectLeaves;

	private Vector3 startingPosition;
	private Vector3 endPosition;
	private bool isMoving;
	private bool leftTheStage;
	private bool isShooting;
	private Transform spawnPoint1;
	private Transform spawnPoint2;
	private Transform leavingPoint1;
	private Transform leavingPoint2;
	private Transform endPoint1;
	private Transform endPoint2;
	private float timerUntilObjectLeavesStore;

	/*Increments with enemy spawns.Resets at half-phase.Determines how much you add to endPos
Look for a better name*/
	private int endPosAdjustment;
	
	public Vector3 adjustmentToEndPosition()
	{
		Vector3 addOn;
		
		if(endPosAdjustment * 0.8f > 5)
		{
			endPosAdjustment = 0;
			addOn = new Vector3( 0.8f * endPosAdjustment, 0.8f, 0);
		}
		else
		{
			addOn = new Vector3( 0.8f * endPosAdjustment, 0, 0);
		}
		
		endPosAdjustment++;
		return addOn;
	}

	private void swapShootingStatus()
	{
		isShooting = !isShooting;
		if(gameObject.tag == "Basic")
		{
			ballisticsScript.enabled = isShooting;
		}
		else if(gameObject.tag == "Cone")
		{
			conePatternScript.enabled = isShooting;
		}
		else if(gameObject.tag == "Boss1")
		{
			boss1PatternScript.enabled = true;
		}
	}

	private void moveObject()
	{
		startingPosition = transform.position;
		if( transform.position != endPosition)
		{
			transform.position = Vector3.MoveTowards(startingPosition, endPosition, speed * Time.deltaTime);
			isShooting = true;
			swapShootingStatus();
		}
		else
		{
			isMoving = false;
			swapShootingStatus();
		}
	}

	private void whichComponentsToGet()
	{
		if(gameObject.tag == "Basic")
		{
			//Debug.Log("Got a basic here!");
			ballisticsScript = GetComponent<Ballistics>();
			ballisticsScript.enabled = false;
		}
		else if(gameObject.tag == "Cone")
		{
			//Debug.Log("Got a cone here!");
			conePatternScript = GetComponent<ConePattern>();
			conePatternScript.enabled = false;
		}
		else if(gameObject.tag == "Boss")
		{
			//Debug.Log("Got a boss here!");
			boss1PatternScript = GetComponent<Boss1Ballistics>();
			boss1PatternScript.enabled = false;
		}
	}

	private void whereTo()
	{
		isMoving = true;
		Vector3 tempPosition = gameObject.transform.position;
		if(tempPosition == spawnPoint1.position)
		{
			endPosition = endPoint1.position + adjustmentToEndPosition();
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else if(tempPosition == spawnPoint2.position)
		{
			endPosition = endPoint2.position - adjustmentToEndPosition();
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else if(tempPosition == GameObject.Find("BossSpawnPoint").transform.position)
		{
			endPosition = GameObject.Find("BossEndPoint").transform.position;
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else
		{
			Debug.LogError("I didn't find my destination!");
		}
	}

	private void backTo()
	{
		isMoving = true;
		leftTheStage = true;
		if(startingPosition == spawnPoint1.position)
		{
			endPosition = leavingPoint1.position;
		}
		if(startingPosition == spawnPoint2.position)
		{
			endPosition = leavingPoint2.position;
		}
	}

	// Use this for initialization
	void Start () 
	{
		startingPosition = transform.position;
		spawnEnemiesScript = GetComponentInParent<SpawnEnemies>();
		spawnPoint1 = GameObject.Find("EnemySpawnPoint1").transform;
		spawnPoint2 = GameObject.Find("EnemySpawnPoint2").transform;
		leavingPoint1 = GameObject.Find("LeavingPoint1").transform;
		leavingPoint2 = GameObject.Find("LeavingPoint2").transform;
		endPoint1 = GameObject.Find("EnemyEndPoint1").transform;
		endPoint2 = GameObject.Find("EnemyEndPoint2").transform;
		timerUntilObjectLeavesStore = timerUntilObjectLeaves;
		startingPosition = spawnEnemiesScript.startPosition;
		isShooting = false;
		whichComponentsToGet();
		whereTo();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isMoving)
		{
			moveObject();
		}
		else
		{
			timerUntilObjectLeaves -= Time.deltaTime;

			if(timerUntilObjectLeaves <= 0)
			{
				backTo();
				timerUntilObjectLeaves = timerUntilObjectLeavesStore;
			}
		}

		if(leftTheStage == true && isMoving == false)
		{
			Destroy(gameObject);
		}
	}
}
