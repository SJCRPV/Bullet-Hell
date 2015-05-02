using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour {

	//No idea what this is for
	GameObject datObject;

	public int healthPoints = 0;
	public float invincibilityTime;

	private float invincibilityTimeStore;
	private int objectLayer;
	
	public int getHealthPoints()
	{
		return healthPoints;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(objectLayer == gameObject.layer && invincibilityTime <= 0)
		{
			Debug.Log("Ow! ; _ ;");
			healthPoints--;
			invincibilityTime = invincibilityTimeStore;
		}
	}

	void Die()
	{
		Debug.Log("DEAD!");
		Destroy(gameObject);
	}
	
	// Use this for initialization
	void Start () {
		objectLayer = gameObject.layer;
		invincibilityTimeStore = invincibilityTime;
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