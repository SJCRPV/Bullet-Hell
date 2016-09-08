using UnityEngine;
using System.Collections;
using System;

public abstract class Character_Boss : Character {

    protected Movement_Boss bossMovementScript;
    protected Movement_Generic genericMovementScript;

    private GameObject blockInstance;

    public override void explode()
    {
        for (int i = 0; i < 20; i++)
        {
            if ((i >= 1 && i < 5) || i >= 15)
            {
                blockInstance = (GameObject)Instantiate(pointBlock, transform.position, Quaternion.identity);
            }
            else if (i >= 5 && i < 15)
            {
                blockInstance = (GameObject)Instantiate(powerBlock, transform.position, Quaternion.identity);
            }
            else if (i == 0)
            {
                blockInstance = (GameObject)Instantiate(extraLifeBlock, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Invalid number. I don't know what block to create with this. 'Tried to resolve the case for " + i);
            }
            blockInstance.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-200f + i * 20, 150), transform.position);
        }
        die();
    }

    public abstract void swapPatterns();

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (invincibilityTime <= 0)
        {
            //Debug.Log("Ow! ; _ ;");
            decreaseHealth();
        }
    }

    public void moveToNextPath()
    {
        SendMessage("moveToNextPath");
    }

    protected new void Start()
    {
        base.Start();
    }
}
