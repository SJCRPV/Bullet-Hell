using UnityEngine;
using System.Collections;

public class LevelDatabase : MonoBehaviour {

	public int currentLevel;
	public int currentLevelPhase;

	public GameObject enemyBasic;
	public GameObject enemyCone;
	public GameObject enemyGraze;
	public GameObject enemyMiniBoss1;
	public GameObject enemyBoss1;

	public enum enemyList
	{
		//Change the names when you figure out what to call them
		Basic = 0,
		Cone = 1,
		Graze = 2,
		Enemy4,
		Enemy5,
		Enemy6,
		Enemy7,
		Enemy8,
		MiniBoss1 = 8,
		MiniBoss2,
		MiniBoss3,
		MiniBoss4,
		MiniBoss5,
		MiniBoss6,
		MiniBoss7,
		MiniBoss8,
		Boss1 = 16,
		Boss2,
		Boss3,
		Boss4,
		Boss5,
		Boss6,
		Boss7,
		Boss8,
	}

	public int[,] levelArray = new int[10, 30];

	void fillArray()
	{
		switch(currentLevel)
		{
		case 1:
			for(int j = 0; j < 10; j++)
			{
				switch(j)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 5:
				case 6:
				case 7:
				case 8:
					for(int i = 1; i < levelArray[j, 0]; i++)
					{
						//TASK: When you figure out which enemies show up in which levels, re-do this
						if(i > levelArray[j,0])
						{
							levelArray[j, i] = 99;
						}
						else if(i % 4 == 0)
						{
							//Debug.Log("Added a Cone in phase " + j + " and position " + i);
							levelArray[j, i] = (int)enemyList.Cone;
						}
						else if(i % 3 == 0)
						{
							//Debug.Log("Added a Graze in phase " + j + " and position " + i);
							levelArray[j, i] = (int)enemyList.Graze;
						}
						else
						{
							//Debug.Log("Added a Basic in phase " + j + " and position " + i);
							levelArray[j, i] = (int)enemyList.Basic;
						}
					}
					break;

				case 4:
					levelArray[4, 1] = (int)enemyList.MiniBoss1;
					break;
				case 9:
					levelArray[9, 1] = (int)enemyList.Boss1;
					break;
				}
			}
		break;


		}
	}

	public void Level0()
	{
		currentLevel = 0;
		Debug.Log ("You loaded level 0!");
	}
	public void Level1()
	{
		Debug.Log("You loaded level 1!");
		currentLevel = 1;
		levelArray[0,0] = 9;
		levelArray[1,0] = 11;
		levelArray[2,0] = 11;
		levelArray[3,0] = 15;
		levelArray[4,0] = 1;
		levelArray[5,0] = 18;
		levelArray[6,0] = 20;
		levelArray[7,0] = 24;
		levelArray[8,0] = 24;
		levelArray[9,0] = 1;
		fillArray();
	}
	public void Level2()
	{
		Debug.Log("You loaded level 2!");
		currentLevel = 2;
		levelArray[0,0] = 5;
		levelArray[1,0] = 18;
		levelArray[2,0] = 18;
		levelArray[3,0] = 14;
		levelArray[4,0] = 1;
		levelArray[5,0] = 18;
		levelArray[6,0] = 20;
		levelArray[7,0] = 24;
		levelArray[8,0] = 28;
		levelArray[9,0] = 1;
	}

	void OnLevelWasLoaded(int level)
	{
		switch(level)
		{
		case 0:
			Level0();
			break;
			
		case 1:
			Level1();
			break;
			
		case 2:
			Level2();
			break;
			
		default:
			break;
		}
	}

	// Use this for initialization
	void Start () {
		currentLevel = 0;
		currentLevelPhase = -1;
	}
}
