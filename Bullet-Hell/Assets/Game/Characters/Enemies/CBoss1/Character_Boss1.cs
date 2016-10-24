using UnityEngine;
using System.Collections.Generic;
using System;

public class Character_Boss1 : Character_Boss {

    Boss1_Pattern1 pattern1Script;
    Boss1_Pattern2 pattern2Script;

    public override void swapPatterns()
    {
        if (bossMovementScript.getCurrentPathNum() == 0)
        {
            pattern1Script.enabled = false;
            bossMovementScript.setReturningToStart();
            pattern2Script.enabled = true;
        }
        else
        {
            Debug.LogError("Tried to set the pattern for the path nº " + bossMovementScript.getCurrentPathNum());
        }
    }

    // Use this for initialization
    protected new void Start()
    {
        base.Start();
        bossMovementScript = GetComponent<Movement_Boss>();
        genericMovementScript = GetComponent<Movement_Generic>();
        pattern1Script = GetComponentInChildren<Boss1_Pattern1>();
        pattern2Script = GetComponentInChildren<Boss1_Pattern2>();
    }
	
	// Update is called once per frame
	void Update () {
        if(genericMovementScript.isActiveAndEnabled && genericMovementScript.getIsMoving() == false)
        {
            bossMovementScript.enabled = true;
            genericMovementScript.enabled = false;
        }
        if (getCurrentHealth() <= 0)
        {
            //Debug.Log(getCurrentHealth());
            explode();
        }
        //CLEANING: This has a hardcoded value. Determine the ratio and use that instead.
        else if (getCurrentHealth() <= getMaxHealth() / 2 && bossMovementScript.getCurrentPathNum() == 0)
        {
            swapPatterns();
        }
    }
}
