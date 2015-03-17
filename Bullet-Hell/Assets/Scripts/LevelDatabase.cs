using UnityEngine;
using System.Collections;

public class LevelDatabase : MonoBehaviour {

	private int[] levelArray = new int[5];


	public int[] Level1()
	{
		levelArray[0] = 10;
		levelArray[1] = 12;
		levelArray[2] = 7;
		levelArray[3] = 12;
		levelArray[4] = 1;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
