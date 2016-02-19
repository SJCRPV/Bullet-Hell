using UnityEngine;
using System.Collections;

public abstract class Level : MonoBehaviour {

    [SerializeField]
    private int numberOfPhases;
    [SerializeField]
    private int miniBossPhase;
    [SerializeField]
    private int bossPhase;

    public int getNumberOfPhases()
    {
        return numberOfPhases;
    }
    public int getMiniBossPhase()
    {
        return miniBossPhase;
    }
    public int getBossPhase()
    {
        return bossPhase;
    }

    public abstract void fillArray();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
