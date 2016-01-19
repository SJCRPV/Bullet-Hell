using UnityEngine;
using System.Collections.Generic;

public class Movement_Boss : Movement {

    public float nextNodeTime;
    public float pathPercentIncrease;
    public List<GameObject> Children;

    iTweenPath[] bossPaths;

    //You will most likely want to rethink this using the Character script
    private MiniBoss1_Pattern1 MiniBoss1_Pattern1Script;
    private Movement movementScript;
    private Vector3[] currentPath;
    private Vector3[] currentNodePair;
    private int currentNodePairInUse;
    private int currentPathNum = 0;
    private float currentNodePairComplete = 0f;
    private float timeUntilNextNodeStore;


    public override void setPath()
    {
        currentPath = bossPaths[currentPathNum].nodes.ToArray();
        if(currentPath == null)
        {
            Debug.LogError("Didn't find anything for the number: " + currentPathNum);
        }
        currentNodePair = new Vector3[] {currentPath[0], currentPath[1]};
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
        setPath();
    }

    void moveToNextNodePair()
    {
        if(currentNodePairInUse + 1 >= currentPath.Length)
        {
            currentNodePairInUse = 0;
        }
        currentNodePair = new Vector3[] {currentPath[currentNodePairInUse++], currentPath[currentNodePairInUse]};
    }

    void move()
    {
        currentNodePairComplete += pathPercentIncrease * Time.deltaTime;
        iTween.PutOnPath(gameObject, currentNodePair, currentNodePairComplete);

        if (currentNodePairComplete >= 1f)
        {
            currentNodePairComplete = 0;
            moveToNextNodePair();
            nextNodeTime = timeUntilNextNodeStore;
        }
    }

	// Use this for initialization
	void Start () {
        iTween.Init(gameObject);
        MiniBoss1_Pattern1Script = GetComponent<MiniBoss1_Pattern1>();
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
