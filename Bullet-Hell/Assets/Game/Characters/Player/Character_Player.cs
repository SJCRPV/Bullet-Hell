using UnityEngine;
using System.Collections;
using System;

public class Character_Player : Character {

    BlockInteraction blockInteractionScript;

    [SerializeField]
    private int setLives;
    private static int staticLives;
    [SerializeField]
    private float setPower;
    private static float staticPower;
    [SerializeField]
    private int setPoints;
    private static float staticPoints;
    private float invincibilityTimeStore;
    private GameObject blockInstance;
    //The power cap variable may be of better use in the BlockInteractionScript
    //public float powerCap;

    public void incrementLives()
    {
        staticLives++;
    }
    public void decrementLives()
    {
        staticLives--;
    }
    public void setLives(int amount)
    {
        staticLives = amount;
    }
    public int getLives()
    {
        return staticLives;
    }

    public void increasePower(float amount)
    {
        staticPower += amount;
    }
    public void decreasePower(float amount)
    {
        if (staticPower - amount >= 0)
        {
            staticPower -= amount;
        }
        else
        {
            staticPower = 0;
        }
    }
    public void setPower(float amount)
    {
        staticPower = amount;
    }
    public float getPower()
    {
        return staticPower;
    }

    public void increasePoints(float amount)
    {
        staticPoints += amount;
    }
    public void decreasePoints(float amount)
    {
        if (staticPoints - amount >= 0)
        {
            staticPoints -= amount;
        }
        else
        {
            staticPoints = 0;
        }
    }
    public void setPoints(float amount)
    {
        staticPoints = amount;
    }
    public float getPoints()
    {
        return staticPoints;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (invincibilityTime <= 0 && collider.gameObject.layer != 12)
        {
            //Debug.Log("Ow! ; _ ;");
            decreaseHealth();
            if (getHealth() <= 0)
            {
                Debug.Log("You were killed by: " + collider.name);
            }
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
