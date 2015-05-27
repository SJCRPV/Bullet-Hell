﻿using UnityEngine;
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
		if(gameObject.transform.parent == spawnPoint1)
		{
			endPosition = endPoint1.position + spawnEnemiesScript.adjustmentToEndPosition();
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else if(gameObject.transform.parent == spawnPoint2)
		{
			endPosition = endPoint2.position - spawnEnemiesScript.adjustmentToEndPosition();
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else if(gameObject.transform.parent = GameObject.Find("BossSpawnPoint").transform)
		{
			endPosition = GameObject.Find("BossEndPoint").transform.position;
			//Debug.Log(gameObject.name + " is moving to: " + endPosition);
			//Debug.Log("isMoving is " + isMoving);
		}
		else
		{
			Debug.LogError("I didn't find my parent!");
		}
	}

	private void backTo()
	{
		isMoving = true;
		leftTheStage = true;
		if(gameObject.transform.parent == spawnPoint1)
		{
			endPosition = leavingPoint1.position;
		}
		if(gameObject.transform.parent == spawnPoint2)
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
