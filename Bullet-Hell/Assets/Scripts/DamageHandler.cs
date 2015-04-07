using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour {

	//No idea what this is for
	GameObject datObject;
	//public PlayerSpawn playerSpawnScript;
	public int healthPoints = 0;
	public float invincibilityTime = 2.0f;
	//public int numLives = 4;

	// Use this for initialization
	void Start () {
//		playerSpawnScript = GetComponent<PlayerSpawn>();
//		if(playerSpawnScript == null)
//		{
//			Debug.LogError("playerSpawnScript is Null!");
//		}
	}

	public int getHealthPoints()
	{
		return healthPoints;
	}

	void OnTriggerEnter2D()
	{
		//If it's placed on the player layer and it's not on invinci frames
		if(gameObject.layer == 8 && invincibilityTime <= 0)
		{
			Debug.Log("Ow! ; _ ;");
			healthPoints--;
			invincibilityTime = 2.0f;
		}
		//If it's on the enemy layer
		else if (gameObject.layer == 9)
		{
			Debug.Log("Ow! ; _ ;");
			healthPoints--;
		}
	}

	void Die()
	{
		Debug.Log("DEAD!");
		Destroy(gameObject);
	}
	void Awake(){
	}

	// Update is called once per frame
	void Update () {
		invincibilityTime -= Time.deltaTime;
		if(healthPoints <= 0)
		{
			Die();
		}
	}
}