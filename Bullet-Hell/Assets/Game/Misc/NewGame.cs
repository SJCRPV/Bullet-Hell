using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//FIX: This will load regardless of where you click
		if (Input.GetMouseButton (0)) 
		{
            SceneManager.LoadScene(1);
		}
	}
}
