using UnityEngine;
using System.Collections;

public class Boss1Movement : MonoBehaviour {

	public float speed;
	public Vector3 endPosition1;
	public Vector3 endPosition2;

	private Vector3 startPosition;
	private Vector3 destination;
	private bool goToEndPos1;
	private bool isMoving;

	void moveSelf()
	{
		if(transform.position != destination)
		{
			transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
		}
		else
		{
			isMoving = false;
			goToEndPos1 = !goToEndPos1;
		}
	}

	Vector3 whereTo()
	{
		isMoving = true;
		if(goToEndPos1)
		{
			destination = endPosition1;
		}
		else
		{
			destination = endPosition2;
		}

		return destination;
	}

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		goToEndPos1 = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isMoving)
		{
			whereTo();
		}
		else
		{
			moveSelf();
		}
	}
}
