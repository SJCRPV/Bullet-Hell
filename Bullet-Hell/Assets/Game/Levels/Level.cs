using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int miniBossPhase;
    [SerializeField]
    private int bossPhase;
    private int[][] levelArray;
    [SerializeField]
    private int[] enemiesPerPhase;


    public int getNumberOfPhases()
    {
        return enemiesPerPhase.Length;
    }
    public int get(int phaseNumber)
    {
        return levelArray[phaseNumber].Length;
    }
    public int getMiniBossPhase()
    {
        return miniBossPhase;
    }
    public int getBossPhase()
    {
        return bossPhase;
    }
    public int getCurrentLevel()
    {
        return currentLevel;
    }
    public int[] getLevelContent()
    {
        return enemiesPerPhase;
    }
    public int getLevelContentAtIndex(int index)
    {
        return enemiesPerPhase[index];
    }
        
    public void setCurrentLevel(int newLevel)
    {
        currentLevel = newLevel;
    }

    private void fillArray()
    {
        
    }

    //public void prepareArray(int arraySize, int[] arrayContents)
    //{
    //    levelArray = new int[arraySize][];
    //    for(int i = 0; i < arraySize; i++)
    //    {
    //        levelArray[i] = new int[arrayContents[i]];
    //        for(int j = 0; j < levelArray[i].Length; j++)
    //        {
    //            levelArray[i][j] = levelContent[j];
    //        }
    //    }
    //}

    public void prepareArray()
    {
        levelArray = new int[enemiesPerPhase.Length][];
        for(int i = 0; i < enemiesPerPhase.Length; i++)
        {
            levelArray[i] = new int[enemiesPerPhase[i]];
            for(int j = 0; j < levelArray[i].Length; j++)
            {
                //levelArray[i][j] = 
            }
        }
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
