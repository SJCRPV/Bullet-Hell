using UnityEngine;
using System.Collections;

public class DamageHandler : MonoBehaviour {

	GameObject datObject;
	public int healthPoints;

	// Use this for initialization
	void Start () {

	}

	void OnTriggerEnter2D()
	{
		Debug.Log("Ow! ; _ ;");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
