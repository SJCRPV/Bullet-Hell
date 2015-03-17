using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	GameObject enemyInstance;

	public GameObject enemyPrefab;
	public float speed = 1f;
	public int health = 3;
	public Transform endPosition;
	public float lerpTime = 1.0f;
	public float percentageComplete;


	private Vector3 startPosition;
	private bool isLerping = true;
	private float timeLerpStart;
	private Vector3 endVector;
	
	
	// Use this for initialization
	void Start () {
		SpawnEnemy();

	}

	public bool isItLerping()
	{
		if(isLerping != null)
		{
			return isLerping;
		}
	}

	void StartLerping () {
		isLerping = true;
		timeLerpStart = Time.time;
		startPosition = transform.position;
		endVector = new Vector3(endPosition.position.x, endPosition.position.y, endPosition.position.z);
	}

	void SpawnEnemy()
	{
		enemyInstance = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
	}

	void ChangePosition()
	{
		if(isLerping)
		{
			percentageComplete = timeLerpStart/lerpTime;
			
			enemyInstance.transform.position = Vector3.Lerp(startPosition, endVector, percentageComplete);
			
			if(percentageComplete >= lerpTime)
			{
				isLerping = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		StartLerping();
		ChangePosition();
	}


}
