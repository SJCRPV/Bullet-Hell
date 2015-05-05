using UnityEngine;
using System.Collections;

public class SpawnBoss : MonoBehaviour {

	LevelDatabase levelDatabaseScript;
	private GameObject enemyInstance;

	public void spawnBoss()
	{
		//Change it so it can understand which level boss it should spawn.
		Debug.Log ("spawning boss1");
		enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyPrefabBoss1, transform.position, Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
