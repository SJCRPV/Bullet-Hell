using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

	private int healthPointsStore;
	private GameObject playerInstance;
	private DamageHandler damageHandlerScript;
	private BlockInteraction blockInteractionScript;

	public GameObject playerPrefab;
	public float respawnTimer;
	public int numLives = 4;
	public int points;
	public float power;
	
	public int getNumLives()
	{
		return numLives;
	}

	void OnGUI()
	{
		if(numLives >= 0)
		{
			GUI.Label(new Rect(10, 0, 100, 30), "Lives: " + numLives);
			GUI.Label(new Rect(10, 20, 100, 30), "Score: " + points);
			GUI.Label(new Rect(10, 40, 100, 30), "Power: " + power);
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
		//Needs to be cast as a GameObject because Instantiate only returns an Object
		if(numLives >= 0)
		{
			playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
			respawnTimer = 3f;
		}
		assignChild();
	}

	// Use this for initialization
	void Start () {
		damageHandlerScript = GetComponent<DamageHandler>();
		if(damageHandlerScript == null)
		{
			//Debug.Log("damageScript is empty");
		}
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
