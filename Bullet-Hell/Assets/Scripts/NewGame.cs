using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Will load regardless of where you click
		if (Input.GetMouseButton (0)) 
		{
			Application.LoadLevel(1);
		}
	}
}
