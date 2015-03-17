using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

	public GameObject playerPrefab;
	GameObject playerInstance;
	float respawnTimer;
	public int numLives = 3;


	// Use this for initialization
	void Start () {
		SpawnPlayer();
	}

	void SpawnPlayer()
	{
		playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if(playerInstance == null && numLives >= 0)
		{
			respawnTimer -= Time.deltaTime;
			numLives--;

			if(respawnTimer <= 0)
			{
				SpawnPlayer();
				respawnTimer = 3f;
			}
		}
		else
		{
			GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 - 25, 100, 50), "Game Over!");
		}
	}
}
