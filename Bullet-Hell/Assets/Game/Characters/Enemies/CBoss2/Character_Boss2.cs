using UnityEngine;
using System.Collections.Generic;
using System;

public class Character_Boss2 : Character_Boss
{
    Boss2_Pattern1 pattern1Script;
    Boss2_Pattern2 pattern2Script;
	Boss2_Pattern3 pattern3Script;

    public override void swapPatterns()
    {
        if(bossMovementScript.getCurrentPathNum() == 0)
        {
            pattern1Script.enabled = false;
            bossMovementScript.setReturningToStart();
            pattern2Script.enabled = true;
        }
        else if(bossMovementScript.getCurrentPathNum() == 1)
        {
            pattern2Script.enabled = false;
            bossMovementScript.setReturningToStart();
            pattern3Script.enabled = true;
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
        pattern1Script = GetComponentInChildren<Boss2_Pattern1>();
        pattern2Script = GetComponentInChildren<Boss2_Pattern2>();
		pattern3Script = GetComponentInChildren<Boss2_Pattern3>();
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
            Debug.Log(getCurrentHealth());
            explode();
        }
		else if((getCurrentHealth() <= (getMaxHealth() / 3) * 2 && bossMovementScript.getCurrentPathNum() == 0) || (getCurrentHealth() <= (getMaxHealth() / 3) && bossMovementScript.getCurrentPathNum() == 1))
        {
            swapPatterns();
        }
    }
}