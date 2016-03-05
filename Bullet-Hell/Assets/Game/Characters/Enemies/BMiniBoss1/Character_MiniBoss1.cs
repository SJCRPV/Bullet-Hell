using UnityEngine;
using System.Collections.Generic;

public class Character_MiniBoss1 : Character_Boss {

    MiniBoss1_Pattern1 pattern1Script;
    MiniBoss1_Pattern2 pattern2Script;

    public List<IFire> firePatternList;

    private float invincibilityTimeStore;
    private GameObject blockInstance;

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
        //What should this explode into?
        die();
    }

    public override void swapPatterns()
    {
        pattern1Script.enabled = false;
        pattern2Script.enabled = true;
    }

    // Use this for initialization
    void Start()
    {
        invincibilityTime = invincibilityTimeStore;
        bossMovementScript = GetComponent<Movement_Boss>();
        pattern1Script = GetComponentInChildren<MiniBoss1_Pattern1>();
        pattern2Script = GetComponentInChildren<MiniBoss1_Pattern2>();
    }

    void Update()
    {
        invincibilityTime -= Time.deltaTime;
        if (getHealth() <= 0)
        {
            Debug.Log(getHealth());
            explode();
        }
        else if(getHealth() <= 50 && bossMovementScript.getCurrentPathNum() == 0)
        {
            moveToNextPath();
            swapPatterns();
        }
    }
}
