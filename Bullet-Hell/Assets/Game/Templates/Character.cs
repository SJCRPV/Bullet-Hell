using UnityEngine;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {
	[SerializeField]
    private int healthPoints;
	[SerializeField]
    private float invincibilityTime;
	[SerializeField]
    private GameObject powerBlock;
	[SerializeField]
    private GameObject pointBlock;
	[SerializeField]
    private GameObject extraLifeBlock;

    private int maxHealth;

    public abstract void explode();

    public void die()
    {
        Debug.Log("DEAD!");
        Destroy(gameObject);
    }

    public void decreaseHealth()
    {
        healthPoints--;
    }
    public void decreaseHealth(int amount)
    {
        healthPoints -= amount;
    }
    public int getCurrentHealth()
    {
        return healthPoints;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }

	// Use this for initialization
	protected void Start () {
        maxHealth = healthPoints;
        Debug.Log("MaxHealth is now " + maxHealth);
	}
}
