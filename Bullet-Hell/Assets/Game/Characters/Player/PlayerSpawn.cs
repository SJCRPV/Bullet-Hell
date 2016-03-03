using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

	private int healthPointsStore;
	private GameObject playerInstance;
	private Character_Player playerCharacterScript;
	private BlockInteraction blockInteractionScript;
    [SerializeField]
	private int numLives = 4;

	public GameObject playerPrefab;
	public float respawnTimer;
	public int points;
	public float power;
	
    public void incrementNumLives()
    {
        numLives++;
    }
	public int getNumLives()
	{
		return numLives;
	}

	void OnGUI()
	{
		if(numLives >= 0)
		{
			GUI.Label(new Rect(10, 0, 100, 30), "Lives: " + numLives);
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
		numLives--;
		if(numLives >= 0)
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
        if(power < 0)
        {
            power = 0;
        }
	}
}
