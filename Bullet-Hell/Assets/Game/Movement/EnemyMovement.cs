using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	SpawnEnemies spawnEnemiesScript;
	FireBasic FireBasicScript;
	FireCone conePatternScript;
	FireGraze grazePatternScript;
	FireBoss1 boss1PatternScript;

	public float speed;
	public float timerUntilObjectLeaves;
	public bool isMoving;
	public bool leftTheStage;
	public bool isShooting;

	private Vector3 endPosition;
    private Vector3[] pathNodes;
    private Vector3[] pathToTake;
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
	private string pathName;
    private int nodeToStartOn;

    public string toString()
    {
        return gameObject.transform.name;
    }
	
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
			FireBasicScript.enabled = isShooting;
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

 //   /*THIS CODE IS TO BE DELETED:
 //   Redo startMovement() and moveObject() to be in line with the solution provided in this link: http://answers.unity3d.com/questions/355075/using-itween-to-move-a-gameobject-from-node-to-nod.html */
 //   private void startMovement()
 //   {
 //       if (pathName != "ignore")
 //       {
 //           iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", speed * 2, "easetype", iTween.EaseType.easeInQuart, "oncomplete", "moveObject"));
 //       }
 //   }

	//private void moveObject()
	//{
 //       transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
 //       isShooting = true;
 //       swapShootingStatus();
 //       if(transform.position == endPosition)
 //       {
 //           isMoving = false;
 //           swapShootingStatus();
 //       }
	//}
 //   /*END OF CODE TO BE DELETED, REPLACEMENT CODE FOLLOWS BELOW*/

    private void assignNodeValues()
    {
        pathNodes = iTweenPath.GetPath(pathName);
    }

    private Vector3[] choseMovement(int pathStart, int pathEnd)
    {
        if(pathName != "ignore")
        {
            pathToTake = new Vector3[Mathf.Abs(pathEnd - pathStart) + 1];
            int sign;
            if (pathStart > pathEnd)
            {
                sign = -1;
            }
            else
            {
                sign = 1;
            }

            for(int i = 0; i < pathToTake.Length; i++)
            {
                pathToTake[i] = pathNodes[pathStart + (sign * i)];
            }
        }

        return pathToTake;
    }

    private void moveObject()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", choseMovement(nodeToStartOn, nodeToStartOn + 1), "time", speed * 2, "easetype", iTween.EaseType.easeInQuart));
        transform.position = Vector3.MoveTowards(transform.position, pathToTake[nodeToStartOn], speed * Time.deltaTime);
        nodeToStartOn++;
        Debug.Log(nodeToStartOn);

        isShooting = true;
        swapShootingStatus();
        if (transform.position == endPosition)
        {
            isMoving = false;
            swapShootingStatus();
        }
    }

    /*END OF REPLACEMENT CODE!*/
    private void whichComponentsToGet()
	{
        switch (gameObject.tag)
        {
            case "Basic":
                //Debug.Log("Got a basic here!");
                FireBasicScript = GetComponent<FireBasic>();
                FireBasicScript.enabled = false;
                break;

            case "Cone":
                //Debug.Log("Got a cone here!");
                conePatternScript = GetComponent<FireCone>();
                conePatternScript.enabled = false;
                break;

            case "Graze":
                //Debug.Log("Got a graze here!");
                grazePatternScript = GetComponent<FireGraze>();
                grazePatternScript.enabled = false;
                break;

            case "MiniBoss1":
                //NEEDS PATTERNS
                break;

            case "Boss1":
                //Debug.Log("Got a boss here!");
                boss1PatternScript = GetComponent<FireBoss1>();
                boss1PatternScript.enabled = false;
                break;

            default:
                Debug.LogError("I don't know what component to get!");
                break;
        }
	}

    private void whereTo()
    {
        isMoving = true;

        if (gameObject.transform.position == spawnPoint1.position)
        {
            endPosition = endPoint1_3.position - adjustmentToEndPosition();
            pathName = "EnemySpawnPath1";
        }
        else if (gameObject.transform.position == spawnPoint2.position)
        {
            endPosition = endPoint2_4.position + adjustmentToEndPosition();
            pathName = "EnemySpawnPath2";
        }
        else if (gameObject.transform.position == spawnPoint3.position)
        {
            endPosition = endPoint1_3.position - adjustmentToEndPosition();
            pathName = "EnemySpawnPath3";
        }
        else if (gameObject.transform.position == spawnPoint4.position)
        {
            endPosition = endPoint2_4.position - adjustmentToEndPosition();
            pathName = "EnemySpawnPath4";
        }
        else if (gameObject.transform.position == spawnPoint5.position)
        {
            endPosition = endPoint5.position - adjustmentToEndPosition();
            pathName = "EnemySpawnPath5";
        }
        else if (gameObject.transform.position == spawnPoint6.position)
        {
            endPosition = endPoint6.position - adjustmentToEndPosition();
            pathName = "EnemySpawnPath6";
        }
        else if (gameObject.transform.position == spawnPoint7.position)
        {
            endPosition = endPoint7.position - adjustmentToEndPosition();
            pathName = "EnemySpawnPath7";
        }
        else if (gameObject.transform.position == spawnPoint8.position)
        {
            endPosition = endPoint8.position - adjustmentToEndPosition();
            pathName = "EnemySpawnPath8";
        }
        else if (gameObject.transform.position == GameObject.Find("ABossSpawnPoint").transform.position)
        {
            endPosition = GameObject.Find("BossEndPoint").transform.position;
            pathName = "ignore";
        }
        else
        {
            Debug.LogError("I didn't find my destination!\n" + toString());
        }
        //Debug.Log(gameObject.name + " is moving to: " + endPosition);
        //Debug.Log("isMoving is " + isMoving);
        assignNodeValues();
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
		spawnEnemiesScript = GetComponentInParent<SpawnEnemies>();
		spawnPoint1 = GameObject.Find("AEnemySpawnPoint1").transform;
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
        endPoint5 = GameObject.Find("EnemyEndPoint5").transform;
        endPoint6 = GameObject.Find("EnemyEndPoint6").transform;
        endPoint7 = GameObject.Find("EnemyEndPoint7").transform;
        endPoint8 = GameObject.Find("EnemyEndPoint8").transform;
		timerUntilObjectLeavesStore = timerUntilObjectLeaves;
        nodeToStartOn = 0;
		isShooting = false;
		whichComponentsToGet();
		whereTo();
        moveObject();
    }
	
	// Update is called once per frame
	void Update ()
	{
		if(leftTheStage == true && isMoving == false)
		{
			Destroy(gameObject);
		}

		if(isMoving && nodeToStartOn <= pathNodes.Length - 1)
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
