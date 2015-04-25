using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	SpawnEnemy spawnEnemyScript;
	Ballistics ballisticsScript;

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
			ballisticsScript.enabled = true;
		}
	}

	private void whereTo()
	{
		isMoving = true;
		if(gameObject.transform.parent == GameObject.Find("EnemySpawnPoint1").transform)
		{
			endPosition = GameObject.Find("EnemyEndPoint1").transform.position + spawnEnemyScript.endPositionAdjustment();
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else if(gameObject.transform.parent == GameObject.Find("EnemySpawnPoint2").transform)
		{
			endPosition = GameObject.Find("EnemyEndPoint2").transform.position - spawnEnemyScript.endPositionAdjustment();
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else
		{
			Debug.Log("I didn't find my parent!");
		}
	}

	// Use this for initialization
	void Start () 
	{
		startingPosition = transform.position;
		spawnEnemyScript = GetComponentInParent<SpawnEnemy>();
		ballisticsScript = GetComponent<Ballistics>();
		ballisticsScript.enabled = false;
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
