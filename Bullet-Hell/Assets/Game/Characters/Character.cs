using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

    public int health;

    //private int layer;

    //public int getLayer()
    //{
    //    return layer;
    //}

    public void decreaseHealth()
    {
        health--;
    }
    public void decreaseHealth(int amount)
    {
        health -= amount;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
