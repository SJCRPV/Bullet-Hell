using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanMoveToNewLevel : MonoBehaviour {

    private int currentLevel;
    private GameDatabase gameDatabaseScript;

    void loadNextLevel()
    {
        currentLevel++;
        Debug.Log(currentLevel);
        SceneManager.LoadScene(currentLevel);
    }

    //// Use this for initialization
    void Start()
    {
        gameDatabaseScript = GameObject.Find("SpawnManager").GetComponent<GameDatabase>();
        currentLevel = gameDatabaseScript.getCurrentLevel();
    }
}
