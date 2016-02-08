using UnityEngine;
using System.Collections;

public class Character_MiniBoss1 : Character {

    BlockInteraction blockInteractionScript;
    Movement_Boss bossMovementScript;

    private float invincibilityTimeStore;
    private GameObject blockInstance;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (invincibilityTime <= 0)
        {
            Debug.Log("Ow! ; _ ;");
            decreaseHealth();
            invincibilityTime = invincibilityTimeStore;
        }
    }

    public override void explode()
    {
        //What should this explode into?
        die();
    }

    // Use this for initialization
    void Start()
    {
        invincibilityTime = invincibilityTimeStore;
        blockInteractionScript = GetComponent<BlockInteraction>();
        bossMovementScript = GetComponent<Movement_Boss>();
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
            SendMessage("moveToNextPath");
        }
    }
}
