using UnityEngine;
using System.Collections;

public class BlockInteraction : MonoBehaviour {

	PlayerSpawn playerSpawnScript;

	public float powerIncrement;
	public int pointIncrement;
	public int score;
	public float power;

	private int objectLayer;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Life")
		{
			playerSpawnScript.numLives++;
		}
		else if(collider.tag == "Power")
		{
			power += powerIncrement;
		}
		else if(collider.tag == "Points")
		{
			score += pointIncrement;
		}
	}

	// Use this for initialization
	void Start () {
		objectLayer = gameObject.layer;
		playerSpawnScript = GetComponent<PlayerSpawn>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
