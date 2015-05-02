using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	SpawnEnemy spawnEnemyScript;
	Ballistics ballisticsScript;
	ConePattern conePatternScript;

	public float speed;

	private Vector3 startingPosition;
	private Vector3 endPosition;
	private bool isMoving;

	private void moveObject()
	{
		startingPosition = transform.position;
		if( transform.position != endPosition)
		{
			transform.position = Vector3.MoveTowards(startingPosition, endPosition, speed * Time.deltaTime);
		}
		else
		{
			isMoving = false;
			if(gameObject.name == GameObject.Find("Enemy01").name)
			{
				ballisticsScript.enabled = true;
			}
			else if(gameObject.name == GameObject.Find("Enemy02").name)
			{
				conePatternScript.enabled = true;
			}
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
	}

	private void whereTo()
	{
		isMoving = true;
		if(gameObject.transform.parent == GameObject.Find("EnemySpawnPoint1").transform)
		{
			endPosition = GameObject.Find("EnemyEndPoint1").transform.position + spawnEnemyScript.adjustmentToEndPosition();
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else if(gameObject.transform.parent == GameObject.Find("EnemySpawnPoint2").transform)
		{
			endPosition = GameObject.Find("EnemyEndPoint2").transform.position - spawnEnemyScript.adjustmentToEndPosition();
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else
		{
			Debug.LogError("I didn't find my parent!");
		}
	}

	// Use this for initialization
	void Start () 
	{
		startingPosition = transform.position;
		spawnEnemyScript = GetComponentInParent<SpawnEnemy>();
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
	}
}
