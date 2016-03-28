using UnityEngine;
using System.Collections;
using System;

public class FireBoss1 : MonoBehaviour, IFire {

	private Boss1_Pattern1 boss1_Pattern1Script;
	private Boss1_Pattern2 boss1_Pattern2Script;
	private Movement_Boss boss1MovementScript;
    private Character_Boss1 boss1CharacterScript;
	private int bossHP;
	
	void Fire()
	{
		if(boss1CharacterScript.getHealth() >= bossHP/2)
		{
			boss1_Pattern1Script.enabled = true;
			boss1_Pattern2Script.enabled = false;
			boss1MovementScript.enabled = false;
		}
		else if(boss1CharacterScript.getHealth() >= bossHP/4 && boss1CharacterScript.getHealth() < bossHP/2)
		{
			boss1_Pattern1Script.enabled = false;
			boss1_Pattern2Script.enabled = true;
			boss1MovementScript.enabled = true;
		}
	}

    public void firePattern()
    {
        Fire();
    }

    public void assignMovement()
    {
        boss1MovementScript = GetComponent<Movement_Boss>();
    }

	// Use this for initialization
	void Start () {
		boss1_Pattern1Script = GetComponentInChildren<Boss1_Pattern1>();
		boss1_Pattern2Script = GetComponentInChildren<Boss1_Pattern2>();
        boss1CharacterScript = GetComponent<Character_Boss1>();
        assignMovement();
		boss1_Pattern1Script.enabled = false;
		boss1_Pattern2Script.enabled = false;
		bossHP = boss1CharacterScript.getHealth();
	}
	
	// Update is called once per frame
	void Update () {
        firePattern();
	}

}
