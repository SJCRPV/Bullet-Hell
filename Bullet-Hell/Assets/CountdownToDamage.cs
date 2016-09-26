using UnityEngine;
using System.Collections;

public class CountdownToDamage : MonoBehaviour {

    [SerializeField]
    private float timeBeforeDamage;
    [SerializeField]
    private float timeToLive;

    private IEnumerator activateDamage()
    {
        gameObject.layer = 11;
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        gameObject.layer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timeBeforeDamage -= Time.deltaTime;

        if(timeBeforeDamage <= 0)
        {
            StartCoroutine("activateDamage");
        }
	}
}
