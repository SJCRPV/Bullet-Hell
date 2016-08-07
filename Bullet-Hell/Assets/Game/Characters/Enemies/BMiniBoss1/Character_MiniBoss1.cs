using UnityEngine;
using System.Collections.Generic;

public class Character_MiniBoss1 : Character_Boss
{

    MiniBoss1_Pattern1 pattern1Script;
    MiniBoss1_Pattern2 pattern2Script;

    public List<IFire> firePatternList;

    private GameObject blockInstance;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (invincibilityTime <= 0)
        {
            //Debug.Log("Ow! ; _ ;");
            decreaseHealth();
        }
    }

    public override void explode()
    {
        for (int i = 0; i < 20; i++)
        {
            if((i >= 1 && i < 5) || i >= 15 )
            {
                blockInstance = (GameObject)Instantiate(pointBlock, transform.position, Quaternion.identity);
            }
            else if(i >= 5 && i < 15)
            {
                blockInstance = (GameObject)Instantiate(powerBlock, transform.position, Quaternion.identity);
            }
            else if(i == 0)
            {
                blockInstance = (GameObject)Instantiate(extraLifeBlock, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Invalid number. I don't know what block to create with this. 'Tried to resolve the case for " + i);
            }
            blockInstance.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-100f + i * 40, 150), transform.position);
        }
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
        else if (getHealth() <= 50 && bossMovementScript.getCurrentPathNum() == 0)
        {
            bossMovementScript.moveToNextPath();
            swapPatterns();
        }
    }
}
