using UnityEngine;
using System.Collections;
using System;

public class Character_Player : Character {

    BlockInteraction blockInteractionScript;

    public float power;
    public float points;
    //The power cap variable may be of better use in the BlockInteractionScript
    //public float powerCap;

    private float invincibilityTimeStore;
    private GameObject blockInstance;

    public void increasePower(float amount)
    {
        power += amount;
    }
    public void decreasePower(float amount)
    {
        power -= amount;
    }

    public void increasePoints(float amount)
    {
        points += amount;
    }
    public void decreasePoints(float amount)
    {
        points -= amount;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (invincibilityTime <= 0)
        {
            Debug.Log("Ow! ; _ ;");
            decreaseHealth();
            invincibilityTime = invincibilityTimeStore;
        }
    }

    public override void explode()
    {
        for (int i = 0; i < 5; i++)
        {
            blockInstance = (GameObject)Instantiate(powerBlock, transform.position, Quaternion.identity);
            blockInstance.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-50f + i * 20, 150), transform.position);
        }

        decreasePower(blockInteractionScript.powerDecrement);
        decreasePoints(blockInteractionScript.pointDecrement);

        die();
    }

    // Use this for initialization
    void Start () {
        invincibilityTime = invincibilityTimeStore;
        blockInteractionScript = GetComponent<BlockInteraction>();
	}

    void Update()
    {
        invincibilityTime -= Time.deltaTime;
        if(getHealth() <= 0)
        {
            explode();
        }
    }
}
