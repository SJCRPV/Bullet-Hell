using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

	private GameObject playerInstance;
	private Character_Player playerCharacterScript;
	private BlockInteraction blockInteractionScript;

	public GameObject playerPrefab;
	public float respawnTimer;

	void OnGUI()
	{
		if(playerCharacterScript.getLives() >= 0)
		{
			GUI.Label(new Rect(10, 0, 100, 30), "Lives: " + playerCharacterScript.getLives());
			GUI.Label(new Rect(10, 20, 100, 30), "Score: " + playerCharacterScript.getPoints());
			GUI.Label(new Rect(10, 40, 100, 30), "Power: " + playerCharacterScript.getPower());
		}
		else
		{
			GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 - 25, 100, 50), "Game Over!");
		}
	}

	private void assignChild()
	{
		if(playerInstance != null)
		{
			playerInstance.transform.parent = this.transform;
		}
	}

	public void SpawnPlayer()
	{
        playerCharacterScript.decrementLives();
		if(playerCharacterScript.getLives() >= 0)
		{
			playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
            playerInstance.name = "Player";
			respawnTimer = 3f;
            playerCharacterScript = GameObject.Find("Player").GetComponent<Character_Player>();
            if (playerCharacterScript == null)
            {
                Debug.Log("playerCharacterScript is empty");
            }
		}
        assignChild();
	}

	void OnLevelWasLoaded()
	{
		SpawnPlayer();
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
		}
	}
}
