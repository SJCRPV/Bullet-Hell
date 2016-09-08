using UnityEngine;
using System.Collections.Generic;

public class Character_MiniBoss1 : Character_Boss
{

    MiniBoss1_Pattern1 pattern1Script;
    MiniBoss1_Pattern2 pattern2Script;

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
        pattern1Script = GetComponentInChildren<MiniBoss1_Pattern1>();
        pattern2Script = GetComponentInChildren<MiniBoss1_Pattern2>();
    }

    void Update()
    {
        invincibilityTime -= Time.deltaTime;
        if (genericMovementScript.isActiveAndEnabled && genericMovementScript.getIsMoving() == false)
        {
            bossMovementScript.enabled = true;
            genericMovementScript.enabled = false;
        }
        if (getCurrentHealth() <= 0)
        {
            Debug.Log(getCurrentHealth());
            explode();
        }
        else if (getCurrentHealth() <= getMaxHealth() / 2 && bossMovementScript.getCurrentPathNum() == 0)
        {
            swapPatterns();
        }
    }
}
