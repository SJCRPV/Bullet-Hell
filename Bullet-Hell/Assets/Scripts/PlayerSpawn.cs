using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

	//The prefab you drag on the Unity editor
	public GameObject playerPrefab;
	//The object itself
	GameObject playerInstance;
	static DamageHandler damageScript;
	//Stores the health points for later use
	int healthPointsStore;
	public float respawnTimer;
	//public int healthPoints;
	public int numLives = 4;

//	public int getHealthPoints()
//	{
//		//Debug.Log(healthPoints);
//		return healthPoints;
//	}
//	public void decreaseHealthPoints()
//	{
//		healthPoints--;
//	}
	public int getNumLives()
	{
		return numLives;
	}

	void OnGUI()
	{
		//GUI.Label(new Rect(10, 0, 100, 30), "Health: " + damageScript.healthPoints);
		GUI.Label(new Rect(10, 0, 100, 30), "Lives: " + numLives);
	}

	public void SpawnPlayer()
	{
		numLives--;
		//Needs to be cast as a GameObject because Instantiate only returns an Object
		if(numLives >= 0)
		{
			playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
			respawnTimer = 3f;
		}
	}

	// Use this for initialization
	void Start () {
		damageScript = GetComponent<DamageHandler>();
		if(damageScript == null)
		{
			//Debug.Log("damageScript is empty");
		}
		SpawnPlayer();
	}

	void Awake() {
	}
	// Update is called once per frame
	void Update () 
	{
		if(playerInstance == null)
		{
			respawnTimer -= Time.deltaTime;
			if( respawnTimer <= 0)
			{
				SpawnPlayer();
			}
			/*else
			{
				GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 - 25, 100, 50), "Game Over!");
				Debug.Log ("Destroyed!");
			}*/
		}
	}
}
