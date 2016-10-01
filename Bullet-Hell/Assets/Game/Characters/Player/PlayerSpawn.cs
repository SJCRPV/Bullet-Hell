using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

	public GameObject playerPrefab;
	public float respawnTimer;

    private GameObject playerInstance;
	private Character_Player playerCharacterScript;
	private BlockInteraction blockInteractionScript;
    private bool isPlayerSpawned = false;

	void OnGUI()
	{
        if (isPlayerSpawned)
        {
            if (playerCharacterScript.canPlayerSpawn())
            {
                GUI.Label(new Rect(10, 0, 100, 30), "Lives: " + playerCharacterScript.getLivesLeft());
                GUI.Label(new Rect(10, 20, 100, 30), "Score: " + playerCharacterScript.getPoints());
                GUI.Label(new Rect(10, 40, 100, 30), "Power: " + System.Math.Round(playerCharacterScript.getPower(), 2));
            }
            else
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Game Over!");
            } 
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
        Debug.Log("initialPlayerSpawn was run");
        playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
        playerInstance.name = "Player";
        respawnTimer = 3f;
        playerCharacterScript = GameObject.Find("Player").GetComponent<Character_Player>();
        playerCharacterScript.GetComponent<SpriteRenderer>().enabled = true;
        playerCharacterScript.setPower(playerCharacterScript.getStaticPower());
        playerCharacterScript.setPoints(playerCharacterScript.getStaticPoints());
        //Debug.Log(playerCharacterScript.getLivesLeft() + " lives left.");
        assignChild();
        isPlayerSpawned = true;
        if (playerCharacterScript == null)
        {
            Debug.LogError("playerCharacterScript is empty");
        }
    }

	public void SpawnPlayer()
	{
        //Debug.Log("SpawnPlayer was run");
        if (playerCharacterScript.canPlayerSpawn())
        {
			playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
            playerInstance.name = "Player";
			respawnTimer = 3f;
            playerCharacterScript = GameObject.Find("Player").GetComponent<Character_Player>();
            playerCharacterScript.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("Setting the staticPower at " + playerCharacterScript.getStaticPower());
            Debug.Log("Setting the staticPoints at " + playerCharacterScript.getStaticPoints());
            playerCharacterScript.setPower(playerCharacterScript.getStaticPower());
            playerCharacterScript.setPoints(playerCharacterScript.getStaticPoints());
            isPlayerSpawned = true;
        }
        if (playerCharacterScript == null)
        {
            Debug.LogError("playerCharacterScript is empty");
        }
        assignChild();
    }

	void Start()
	{
        if (playerInstance == null)
        {
            initialPlayerSpawn();
        }
	}

	// Update is called once per frame
	void Update () 
	{
		if(playerInstance == null)
		{
			respawnTimer -= Time.deltaTime;
            isPlayerSpawned = false;
			if( respawnTimer <= 0)
			{
				SpawnPlayer();
			}
		}
	}
}
