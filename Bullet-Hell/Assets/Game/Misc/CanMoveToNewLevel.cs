using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanMoveToNewLevel : MonoBehaviour {

    //private bool detectedBoss;
    //private bool bossIsAlive;
    //public float timeUntilNextLevel;
    private int currentLevel;
    private GameDatabase gameDatabaseScript;

    void loadNextLevel()
    {
        currentLevel++;
        Debug.Log(currentLevel);
        Destroy(GameObject.Find("Player"));
        //TODO: Bring the player's power and points into the new level
        SceneManager.LoadScene(currentLevel);
    }

    //// Use this for initialization
    void Start()
    {
        gameDatabaseScript = GameObject.Find("SpawnManager").GetComponent<GameDatabase>();
        currentLevel = gameDatabaseScript.getCurrentLevel();
    }
}
