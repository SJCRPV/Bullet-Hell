using UnityEngine;
using System.Collections;
using System;

public class Character_Player : Character {

    BlockInteraction blockInteractionScript;

    [SerializeField]
    private int numLives;
    private static int livesLost;
    [SerializeField]
    private float numPower;
    private static float staticPower;
    [SerializeField]
    private float numPoints;
    private static float staticPoints;
    private float invincibilityTimeStore;
    private GameObject blockInstance;

    public int getLivesLeft()
    {
        return numLives - livesLost;
    }
    public bool canPlayerSpawn()
    {
        if(numLives - livesLost > 0)
        {
            return true;
        }
        return false;
    }
    public int getLivesLost()
    {
        return livesLost;
    }
    public int getLives()
    {
        return numLives;
    }
    public void incrementLives()
    {
        livesLost--;
    }
    public void setLives(int amount)
    {
        numLives = amount;
    }

    public void increasePower(float amount)
    {
        numPower += amount;
    }
    public void decreasePower(float amount)
    {
        if (numPower - amount >= 0)
        {
            numPower -= amount;
        }
        else
        {
            numPower = 0;
        }
    }
    public void setPower(float amount)
    {
        numPower = amount;
    }
    public void setStaticPower(float amount)
    {
        staticPower = amount;
        Debug.Log("staticPower is now: " + staticPower);
    }
    public float getPower()
    {
        return numPower;
    }
    public float getStaticPower()
    {
        return staticPower;
    }

    public void increasePoints(float amount)
    {
        numPoints += amount;
    }
    public void decreasePoints(float amount)
    {
        if (numPoints - amount >= 0)
        {
            numPoints -= amount;
        }
        else
        {
            numPoints = 0;
        }
    }
    public void setPoints(float amount)
    {
        numPoints = amount;
    }
    public void setStaticPoints(float amount)
    {
        staticPoints = amount;
        Debug.Log("staticPoints is now: " + staticPoints);
    }
    public float getPoints()
    {
        return numPoints;
    }
    public float getStaticPoints()
    {
        return staticPoints;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (getInvencibilityTime() <= 0 && collider.gameObject.layer != 12)
        {
            //Debug.Log("Ow! ; _ ;");
            decreaseHealth();
            if (getCurrentHealth() <= 0)
            {
                Debug.Log("You were killed by: " + collider.name);
            }
            setInvencibilityTime(invincibilityTimeStore);
        }
    }

    public override void explode()
    {
        for (int i = 0; i < 5; i++)
        {
            blockInstance = (GameObject)Instantiate(getPowerBlock(), transform.position, Quaternion.identity);
            blockInstance.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-50f + i * 20, 150), transform.position);
        }

        decreasePower(blockInteractionScript.powerDecrement);
        decreasePoints(blockInteractionScript.pointDecrement);

        setStaticPower(getPower());
        setStaticPoints(getPoints());

        livesLost++;
        die();
    }

    // Use this for initialization
    protected new void Start()
    {
        base.Start();
        invincibilityTimeStore = getInvencibilityTime();
        blockInteractionScript = GetComponentInChildren<BlockInteraction>();
	}

    void Update()
    {
        decrementInvincibilityTime(Time.deltaTime);
        if(getCurrentHealth() <= 0)
        {
            explode();
        }
    }
}
