using UnityEngine;
using System.Collections;
using System;

public abstract class Character_Boss : Character {

    protected Movement_Boss bossMovementScript;

    public abstract override void explode();

    public abstract void swapPatterns();

    public void moveToNextPath()
    {
        SendMessage("moveToNextPath");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
