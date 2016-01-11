using UnityEngine;
using System.Collections;
using System;

public class FireBoss1 : MonoBehaviour, IFire {

	private Boss1_Pattern1 boss1_Pattern1Script;
	private Boss1_Pattern2 boss1_Pattern2Script;
	private Boss1Movement boss1MovementScript;
	private DamageHandler damageHandlerScript;
	private int bossHP;
	
	void Fire()
	{
		if(damageHandlerScript.getHealthPoints() >= bossHP/2)
		{
			boss1_Pattern1Script.enabled = true;
			boss1_Pattern2Script.enabled = false;
			boss1MovementScript.enabled = false;
		}
		else if(damageHandlerScript.getHealthPoints() >= bossHP/4 && damageHandlerScript.getHealthPoints() < bossHP/2)
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

	// Use this for initialization
	void Start () {
		boss1_Pattern1Script = GetComponent<Boss1_Pattern1>();
		boss1_Pattern2Script = GetComponent<Boss1_Pattern2>();
		boss1MovementScript = GetComponent<Boss1Movement>();
		damageHandlerScript = GetComponent<DamageHandler>();
		boss1_Pattern1Script.enabled = false;
		boss1_Pattern2Script.enabled = false;
		bossHP = damageHandlerScript.getHealthPoints();
	}
	
	// Update is called once per frame
	void Update () {
        firePattern();
	}

}
