using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour {

	BlockInteraction blockInteractionScript;

	public int healthPoints;
	public float invincibilityTime;
	public GameObject pointsBlock;
	public GameObject powerBlock;
	public GameObject extraLifeBlock;

	private float invincibilityTimeStore;
	private int oppositeLayer;
	private GameObject blockInstance;

	public int getHealthPoints()
	{
		return healthPoints;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if((oppositeLayer == 19 || oppositeLayer == 20) && invincibilityTime <= 0)
		{
			Debug.Log("Ow! ; _ ;");
			healthPoints--;
			invincibilityTime = invincibilityTimeStore;
		}
	}

	void explode()
	{
		if(gameObject.tag == "Player")
		{
			for(int i = 0; i < 5; i++)
			{
				blockInstance = (GameObject)Instantiate(powerBlock, transform.position, Quaternion.identity);
				blockInstance.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-2.5f + i, Vector2.up), transform.position);
			}
		}
	}

	void Die()
	{
		Debug.Log("DEAD!");
		explode();
		Destroy(gameObject);
	}
	
	// Use this for initialization
	void Start () {
		blockInteractionScript = GetComponent<BlockInteraction>();
		if(gameObject.layer == 10 || gameObject.layer == 8)
		{
			oppositeLayer = 19;
		}
		else if(gameObject.layer == 11 || gameObject.layer == 9)
		{
			oppositeLayer = 20;
		}
		else
		{
			oppositeLayer = gameObject.layer;
			Debug.Log("Unexpected layer. oppositeLayer has a value of: " + oppositeLayer);
		}
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