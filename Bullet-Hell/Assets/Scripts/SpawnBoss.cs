using UnityEngine;
using System.Collections;

public class SpawnBoss : MonoBehaviour {

	LevelDatabase levelDatabaseScript;
	//Transform 
	private GameObject enemyInstance;

	private bool bossSpawned;

	public void spawnBoss()
	{
		//Change it so it can understand which level boss it should spawn.
		Debug.Log ("spawning boss1");
		enemyInstance = (GameObject)Instantiate(levelDatabaseScript.enemyPrefabBoss1, GameObject.Find("BossSpawnPoint").transform.position, Quaternion.identity);
		bossSpawned = true;
		enemyInstance.name = "Boss";
	}

	// Use this for initialization
	void Start () {
		levelDatabaseScript = GetComponent<LevelDatabase> ();
		bossSpawned = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(levelDatabaseScript.currentLevelPhase + 1 == levelDatabaseScript.levelArray.Length && bossSpawned == false)
		{
			spawnBoss();
		}
	}
}
