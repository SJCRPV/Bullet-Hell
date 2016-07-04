using UnityEngine;
using System.Collections.Generic;
using System;

public class Character_Boss1 : Character_Boss {

    Boss1_Pattern1 pattern1Script;
    Boss1_Pattern2 pattern2Script;

    public List<IFire> firePatternList;

    private float invincibilityTimeStore;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (invincibilityTime <= 0)
        {
            //Debug.Log("Ow! ; _ ;");
            decreaseHealth();
            invincibilityTime = invincibilityTimeStore;
        }
    }

    public override void explode()
    {
        //Needs to explode into something
        die();
    }

    public override void swapPatterns()
    {
        pattern1Script.enabled = false;
        bossMovementScript.setReturningToStart();
        pattern2Script.enabled = true;
    }

    // Use this for initialization
    void Start () {
        invincibilityTime = invincibilityTimeStore;
        bossMovementScript = GetComponent<Movement_Boss>();
        pattern1Script = GetComponentInChildren<Boss1_Pattern1>();
        pattern2Script = GetComponentInChildren<Boss1_Pattern2>();
    }
	
	// Update is called once per frame
	void Update () {
        invincibilityTime -= Time.deltaTime;
        if (getHealth() <= 0)
        {
            Debug.Log(getHealth());
            explode();
        }
        else if (getHealth() <= 50 && bossMovementScript.getCurrentPathNum() == 0)
        {
            swapPatterns();
        }
    }
}
