using UnityEngine;
using System.Collections;
using System;

public class Character_Player : Character {

    BlockInteraction blockInteractionScript;

    [SerializeField]
    private float power;
    [SerializeField]
    private float points;
    private float invincibilityTimeStore;
    private GameObject blockInstance;
    //The power cap variable may be of better use in the BlockInteractionScript
    //public float powerCap;

    public void increasePower(float amount)
    {
        power += amount;
    }
    public void decreasePower(float amount)
    {
        power -= amount;
    }
    public float getPower()
    {
        return power;
    }

    public void increasePoints(float amount)
    {
        points += amount;
    }
    public void decreasePoints(float amount)
    {
        points -= amount;
    }
    public float getPoints()
    {
        return points;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
        if (invincibilityTime <= 0 && collider.gameObject.layer != 12)
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
        invincibilityTimeStore = invincibilityTime;
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
