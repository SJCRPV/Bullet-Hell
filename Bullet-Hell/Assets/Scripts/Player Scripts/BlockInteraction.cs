using UnityEngine;
using System.Collections;

public class BlockInteraction : MonoBehaviour {

	PlayerSpawn playerSpawnScript;

	public float powerIncrement;
	public int pointIncrement;
	public float powerDecrement;
	public int pointDecrement;
	public float powerCap;

	private int objectLayer;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Life")
		{
			playerSpawnScript.numLives++;
		}
		else if(collider.tag == "Power")
		{
			if(playerSpawnScript.power <= powerCap)
			{
				playerSpawnScript.power += powerIncrement;
			}
		}
		else if(collider.tag == "Points")
		{
			playerSpawnScript.points += pointIncrement;
		}
	}

	// Use this for initialization
	void Start () {
		objectLayer = gameObject.layer;
		playerSpawnScript = GetComponentInParent<PlayerSpawn>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
