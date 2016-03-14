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

    private void initialPlayerSpawn()
    {
        playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
        playerInstance.name = "Player";
        respawnTimer = 3f;
        playerCharacterScript = GameObject.Find("Player").GetComponent<Character_Player>();
        Debug.Log(playerCharacterScript.getLives() + " lives left.");
        assignChild();
        if (playerCharacterScript == null)
        {
            Debug.Log("playerCharacterScript is empty");
        }
    }
	public void SpawnPlayer()
	{
        Debug.Log(playerCharacterScript.getLives() + " lives left.");
		if(playerCharacterScript.getLives() >= 0)
		{
			playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
            playerInstance.name = "Player";
			respawnTimer = 3f;
            playerCharacterScript = GameObject.Find("Player").GetComponent<Character_Player>();
            playerCharacterScript.setLives(playerCharacterScript.getStaticLives());
            playerCharacterScript.setPower(playerCharacterScript.getStaticPower());
            playerCharacterScript.setPoints(playerCharacterScript.getStaticPoints());
		}
        if (playerCharacterScript == null)
        {
            Debug.Log("playerCharacterScript is empty");
        }
        assignChild();
	}

	void OnLevelWasLoaded()
	{
		initialPlayerSpawn();
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
