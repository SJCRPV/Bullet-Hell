using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour {

	BlockInteraction blockInteractionScript;
	PlayerSpawn playerSpawnScript;
	CircleCollider2D circleCollider;
	CircleCollider2D circleColliderChild;

	public int healthPoints;
	public float invincibilityTime;
	public GameObject pointsBlock;
	public GameObject powerBlock;
	public GameObject extraLifeBlock;

	private float invincibilityTimeStore;
	private int oppositeLayer;
	private GameObject blockInstance;
    private Vector3 positionOnDeath;
	private int blockLayer;

	public int getHealthPoints()
	{
		return healthPoints;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		//if((oppositeLayer == 19 || oppositeLayer == 20) && invincibilityTime <= 0)
		if((collider.gameObject.layer == 8 || collider.gameObject.layer == 9 || collider.gameObject.layer == 10 || collider.gameObject.layer == 11 ) && invincibilityTime <= 0)
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

	void explode()
	{
        positionOnDeath = transform.position;
		if(gameObject.tag == "Player")
		{
			for(int i = 0; i < 5; i++)
			{
				blockInstance = (GameObject)Instantiate(powerBlock, transform.position, Quaternion.identity);
				blockInstance.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-50f + i*20, 150), transform.position);
			}
			playerSpawnScript.power -= blockInteractionScript.powerDecrement;
			playerSpawnScript.points -= blockInteractionScript.pointDecrement;
		}
        if(gameObject.layer == 9)
        {
            for(int i = 0; i < 10; i++)
            {
                switch(i)
                {
                    case 0:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 9:
                        blockInstance = (GameObject)Instantiate(pointsBlock, transform.position, Quaternion.identity);
                        break;

                    case 1:
                    case 8:
                        blockInstance = (GameObject)Instantiate(powerBlock, transform.position, Quaternion.identity);
                        break;
                }
                blockInstance.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-100f + i * 40, 150), transform.position);
            }
        }
        Die();
	}

	
	// Use this for initialization
	void Start () {
		blockInteractionScript = GetComponent<BlockInteraction>();
		playerSpawnScript = GetComponentInParent<PlayerSpawn>();
		//circleCollider = this.GetComponent<CircleCollider2D>();
		//circleColliderChild = GetComponentInChildren<CircleCollider2D>();
		//Physics2D.IgnoreCollision(circleCollider, circleColliderChild, true);
		//Player layers
		if(gameObject.layer == 10 || gameObject.layer == 8)
		{
			oppositeLayer = 19;
		}
		//Enemy layers
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
			explode();
		}
	}
}