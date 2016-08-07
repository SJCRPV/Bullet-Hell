using UnityEngine;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {
    //CLEANING: Refactor this to make public variables private and make gets and sets
    public int healthPoints;
    public float invincibilityTime;
    public GameObject powerBlock;
    public GameObject pointBlock;
    public GameObject extraLifeBlock;

    //private int layer;

    //public int getLayer()
    //{
    //    return layer;
    //}

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
    public int getHealth()
    {
        return healthPoints;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
