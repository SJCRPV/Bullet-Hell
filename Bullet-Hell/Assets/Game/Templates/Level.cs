using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    [SerializeField]
    private int level;
    [SerializeField]
    private int miniBossPhase;
    [SerializeField]
    private int bossPhase;
    [SerializeField]
    private int[] enemiesPerPhase;

    private int[][] levelArray;

    //CONSIDER: Make a get method that returns the full lenght of the 2D array
    public int getNumberOfPhases()
    {
        return levelArray.GetLength(0);
    }
    public int getPhaseLenght(int phaseNumber)
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
    public int getLevelNum()
    {
        return level;
    }
    public int[] getLevelContent()
    {
        return enemiesPerPhase;
    }
    public int getLevelContentAtIndex(int index)
    {
        return enemiesPerPhase[index];
    }
    public int getSpecificContentAtIndex(int phase, int positionInPhase)
    {
        return levelArray[phase][positionInPhase];
    }
        
    public void setCurrentLevel(int newLevel)
    {
        level = newLevel;
    }

    public void prepareArray()
    {
        levelArray = new int[enemiesPerPhase.Length][];
        //Debug.Log("The length of enemisPerPhase is: " + enemiesPerPhase.Length);
        //Debug.Log("The length of levelArray is: " + levelArray.Length);
        for (int i = 0; i < enemiesPerPhase.Length; i++)
        {
            levelArray[i] = new int[enemiesPerPhase[i]];
            //Debug.Log("At phase " + i + ", its length is: " + levelArray[i].Length);
            for(int j = 0; j < levelArray[i].Length; j++)
            {
                if (i == miniBossPhase)
                {
                    //Debug.Log("Added MiniBoss1 in phase " + i + " and position " + j);
                    levelArray[i][0] = (int)enemyList.MiniBoss1;
                }
                else if(i == bossPhase)
                {
                    //Debug.Log("Added Boss1 in phase " + i + " and position " + j);
                    levelArray[i][0] = (int)enemyList.Boss1;
                }
                else if(j != bossPhase || j != miniBossPhase)
                {
                    int k = j % 3;
                    if(k == 0)
                    {
                        //Debug.Log("Added a Basic in phase " + i + " and position " + j);
                        levelArray[i][j] = (int)enemyList.Basic;
                    }
                    else if(k == 1)
                    {
                        //Debug.Log("Added a Cone in phase " + i + " and position " + j);
                        levelArray[i][j] = (int)enemyList.Cone;
                    }
                    else if(k == 2)
                    {
                        //Debug.Log("Added a Graze in phase " + i + " and position " + j);
                        levelArray[i][j] = (int)enemyList.Graze;
                    }
                    else
                    {
                        Debug.LogError("I didn't find what to do k is equal to " + k);
                    }
                }
                else
                {
                    Debug.LogError("I don't know what to do when j is equal to " + j);
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        prepareArray();
	}
}
