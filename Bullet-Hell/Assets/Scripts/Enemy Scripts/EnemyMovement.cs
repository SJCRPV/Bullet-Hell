using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	SpawnEnemies spawnEnemiesScript;
	Ballistics ballisticsScript;
	ConePattern conePatternScript;
	GrazePattern grazePatternScript;
	Boss1Ballistics boss1PatternScript;

	public float speed;
	public float timerUntilObjectLeaves;
	public bool isMoving;
	public bool leftTheStage;
	public bool isShooting;

	private Vector3 startingPosition;
	private Vector3 endPosition;
	private Transform spawnPoint1;
	private Transform spawnPoint2;
	private Transform spawnPoint3;
	private Transform spawnPoint4;
	private Transform spawnPoint5;
	private Transform spawnPoint6;
	private Transform spawnPoint7;
	private Transform spawnPoint8;
	private Transform leavingPoint1;
	private Transform leavingPoint2;
	private Transform endPoint1_3;
	private Transform endPoint2_4;
	private Transform endPoint5;
	private Transform endPoint6;
	private Transform endPoint7;
	private Transform endPoint8;
	private float timerUntilObjectLeavesStore;
	private float endPosAdjustment;
    private Rigidbody2D rigidBody;
	private string pathName;


	
	public Vector3 adjustmentToEndPosition()
	{
		endPosAdjustment = spawnEnemiesScript.endPosAdjustment;
		Vector3 addOn;

		//Have the endPosition depend on whether the current phase is even or pair
		if(spawnEnemiesScript.levelDatabaseScript.currentLevelPhase % 2 == 0)
		{
			addOn = new Vector3( 1f * endPosAdjustment, 0, 0);
			//Debug.Log("addOn was given the value of: " + addOn);
		}
		else
		{
			addOn = new Vector3( 1f * endPosAdjustment + 1, 1, 0);
			//Debug.Log("addOn was given the value of: " + addOn);
		}
		
		spawnEnemiesScript.endPosAdjustment++;
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
		else if(gameObject.tag == "Graze")
		{
			grazePatternScript.enabled = isShooting;
		}
		else if(gameObject.tag == "Boss1")
		{
			boss1PatternScript.enabled = true;
		}
	}

    private void startMovement()
    {
        if (pathName != "ignore")
        {
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", speed * 2, "easetype", iTween.EaseType.easeInQuart, "oncomplete", "moveObject"));
        }
    }

	private void moveObject()
	{
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        isShooting = true;
        swapShootingStatus();
        if(transform.position == endPosition)
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
		else if(gameObject.tag == "Graze")
		{
			//Debug.Log("Got a graze here!");
			grazePatternScript = GetComponent<GrazePattern>();
			grazePatternScript.enabled = false;
		}
		else if(gameObject.tag == "Boss1")
		{
			//Debug.Log("Got a boss here!");
			boss1PatternScript = GetComponent<Boss1Ballistics>();
			boss1PatternScript.enabled = false;
		}
        else
        {
            Debug.LogError("I don't know what component to get!");
        }
	}
	
	private void whereTo()
	{
		isMoving = true;
		switch(gameObject.transform.position)
		{
		case spawnPoint1.position:
			endPosition = endPoint1_3.position - adjustmentToEndPosition();
			pathName = "EnemySpawnPath1";
			break;

		case spawnPoint2.position:
			endPosition = endPoint2_4.position + adjustmentToEndPosition();
			pathName = "EnemySpawnPath2";
			break;

		case spawnPoint3.position:
			endPosition = endPoint1_3.position - adjustmentToEndPosition();
			pathName = "EnemySpawnPath3";
			break;

		case spawnPoint4.position:
			endPosition = endPoint2_4.position - adjustmentToEndPosition();
			pathName = "EnemySpawnPath4";
			break;

		case spawnPoint5.position:
			endPosition = endPoint5.position - adjustmentToEndPosition();
			pathName = "EnemySpawnPath5";
			break;

		case spawnPoint6.position:
			endPosition = endPoint6.position - adjustmentToEndPosition();
			pathName = "EnemySpawnPath6";
			break;

		case spawnPoint7.position:
			endPosition = endPoint7.position - adjustmentToEndPosition();
			pathName = "EnemySpawnPath7";
			break;

		case spawnPoint8.position:
			endPosition = endPoint8.position - adjustmentToEndPosition();
			pathName = "EnemySpawnPath8";
			break;

		case GameObject.Find("BossSpawnPoint").transform.position:
			endPosition = GameObject.Find("BossEndPoint").transform.position;
			pathName = "ignore";
			break;

		default:
			Debug.LogError("I didn't find my destination!");
			break;
		}
		//Debug.Log(gameObject.name + " is moving to: " + endPosition);
		//Debug.Log("isMoving is " + isMoving);
	}

	private void backTo()
	{
		isMoving = true;
		leftTheStage = true;
		//This is dirty. See if you can find a way to fix it
		if(transform.position.y == endPoint1_3.position.y || transform.position.y == endPoint1_3.position.y - 1)
		{
			endPosition = leavingPoint1.position;
		}
		if (transform.position.y == endPoint2_4.position.y || transform.position.y == endPoint2_4.position.y + 1)
		{
			endPosition = leavingPoint2.position;
		}
		if (transform.position.y == endPoint5.position.y || transform.position.y == endPoint5.position.y + 1)
		{
			endPosition = leavingPoint1.position;
		}
		if (transform.position.y == endPoint6.position.y || transform.position.y == endPoint6.position.y + 1)
		{
			endPosition = leavingPoint2.position;
		}
		if (transform.position.y == endPoint7.position.y || transform.position.y == endPoint7.position.y + 1)
		{
			endPosition = leavingPoint2.position;
		}
		if (transform.position.y == endPoint8.position.y || transform.position.y == endPoint8.position.y + 1)
		{
			endPosition = leavingPoint1.position;
		}

		//Debug.Log(gameObject.name + " is returning to " + endPosition);
	}

	// Use this for initialization
	void Start () 
	{
		startingPosition = transform.position;
		spawnEnemiesScript = GetComponentInParent<SpawnEnemies>();
		spawnPoint1 = GameObject.Find("EnemySpawnPoint1").transform;
		spawnPoint2 = GameObject.Find("EnemySpawnPoint2").transform;
		spawnPoint3 = GameObject.Find("EnemySpawnPoint3").transform;
		spawnPoint4 = GameObject.Find("EnemySpawnPoint4").transform;
		spawnPoint5 = GameObject.Find("EnemySpawnPoint5").transform;
		spawnPoint6 = GameObject.Find("EnemySpawnPoint6").transform;
		spawnPoint7 = GameObject.Find("EnemySpawnPoint7").transform;
		spawnPoint8 = GameObject.Find("EnemySpawnPoint8").transform;
		leavingPoint1 = GameObject.Find("LeavingPoint1").transform;
		leavingPoint2 = GameObject.Find("LeavingPoint2").transform;
		endPoint1_3 = GameObject.Find("EnemyEndPoint1/3").transform;
		endPoint2_4 = GameObject.Find("EnemyEndPoint2/4").transform;
        rigidBody = GetComponent<Rigidbody2D>();
		timerUntilObjectLeavesStore = timerUntilObjectLeaves;
		isShooting = false;
		whichComponentsToGet();
		whereTo();
        startMovement();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(leftTheStage == true && isMoving == false)
		{
			Destroy(gameObject);
		}

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
	}
}
