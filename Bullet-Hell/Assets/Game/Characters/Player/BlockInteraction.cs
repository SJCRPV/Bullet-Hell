using UnityEngine;
using System.Collections;

public class BlockInteraction : MonoBehaviour {

	PlayerSpawn playerSpawnScript;
    Character_Player playerCharacterScript;

	public float powerIncrement;
	public int pointIncrement;
	public float powerDecrement;
	public int pointDecrement;
	public float powerCap;

	//private int objectLayer;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Life")
		{
			playerSpawnScript.incrementNumLives();
		}
		else if(collider.tag == "Power")
		{
			if(playerSpawnScript.power <= powerCap)
			{
                playerCharacterScript.increasePower(powerIncrement);
			}
		}
		else if(collider.tag == "Points")
		{
            playerCharacterScript.increasePoints(pointIncrement);
		}
	}

	// Use this for initialization
	void Start () {
		//objectLayer = gameObject.layer;
		playerSpawnScript = GetComponentInParent<PlayerSpawn>();
        playerCharacterScript = GetComponent<Character_Player>();
	}
}
