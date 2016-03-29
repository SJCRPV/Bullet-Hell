using UnityEngine;
using System.Collections.Generic;

public class Movement_Boss : Movement {

    public float nextNodeTime;
    public float pathPercentIncrease;
    public List<GameObject> Children;

    iTweenPath[] bossPaths;

    private Movement movementScript;
    private Vector3[] currentPath;
    private Vector3[] currentNodeTrio;
    private int currentNodeTrioInUse = 0;
    private int currentPathNum = 0;
    private float currentNodeTrioComplete = 0f;
    private float timeUntilNextNodeStore;

    public int getCurrentPathNum()
    {
        return currentPathNum;
    }
    public int getCurrentNodePairInUse()
    {
        return currentNodeTrioInUse;
    }

    public override void setPath()
    {
        currentPath = bossPaths[currentPathNum].nodes.ToArray();
        if(currentPath == null)
        {
            Debug.LogError("Didn't find anything for the number: " + currentPathNum);
        }
        currentNodeTrio = new Vector3[] {currentPath[currentNodeTrioInUse++], currentPath[currentNodeTrioInUse++], currentPath[currentNodeTrioInUse]};
    }
    
    void preparePaths()
    {
        //What you want here is for the script to gather an array with all the children GameObjects of the parent object and then iterate through said array to define both the array size and what each index reieves. This allows you flexibility in the boss phase count.
        foreach (Transform child in transform)
        {
            Children.Add(child.gameObject);
        }

        bossPaths = new iTweenPath[gameObject.transform.childCount];
        for (int i = 0; i < bossPaths.Length; i++)
        {
            bossPaths[i] = GameObject.Find(Children[i].gameObject.name).GetComponent<iTweenPath>();
        }
    }

    //This is supposed to be a reciever of a message coming from a Character-like script
    public void moveToNextPath()
    {
        currentPathNum++;
        Debug.Log("Moving to path " + (currentPathNum + 1));
        currentNodeTrioInUse = 0;
        setPath();
    }

    void moveToNextNodePair()
    {
        if(currentNodeTrioInUse + 1 >= currentPath.Length)
        {
            currentNodeTrioInUse = 0;
        }
        currentNodeTrio = new Vector3[] {currentPath[currentNodeTrioInUse++], currentPath[currentNodeTrioInUse++], currentPath[currentNodeTrioInUse]};
    }

    void move()
    {
        currentNodeTrioComplete += pathPercentIncrease * Time.deltaTime;
        //If you put currentPath isntead of currentNodePair, it'll traverse the entire path with the curves.
        iTween.PutOnPath(gameObject, currentNodeTrio, currentNodeTrioComplete);

        if (currentNodeTrioComplete >= 1f)
        {
            currentNodeTrioComplete = 0;
            moveToNextNodePair();
            nextNodeTime = timeUntilNextNodeStore;
        }
    }

	// Use this for initialization
	void Start () {
        iTween.Init(gameObject);
        movementScript = GetComponent<Movement_Generic>();
        timeUntilNextNodeStore = nextNodeTime;
        preparePaths();
        setPath();
	}
	
	// Update is called once per frame
	void Update () {
        if (movementScript.getIsMoving() == false)
        {
            nextNodeTime -= Time.deltaTime;
        }
        setIsMoving(false);
        if(nextNodeTime <= 0)
        {
            setIsMoving(true);
            move();
        }
	}
}
