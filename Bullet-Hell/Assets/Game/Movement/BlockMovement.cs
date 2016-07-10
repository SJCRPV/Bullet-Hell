using UnityEngine;
using System.Collections;

public class BlockMovement : MonoBehaviour {

	SelfDestruct selfDesctructionScript;
	Vector3 startPosition;
	Vector3 playerPosition;
	bool gravitatingTowardsPlayer;

    public float gravityScale;
	public float speed;
    public float timerUntilDestruction;

    public void OnBecameInvisible()
    {
        if(transform.position.y < playerPosition.y)
        {
            Destroy(gameObject);
        }
    }

    void moveToPlayer()
	{
		transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed*Time.deltaTime);
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if(transform.position == playerPosition)
		{
			selfDesctructionScript.obliteration();
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.gameObject.layer == 8)
		{
			gravitatingTowardsPlayer = true;
		}
	}

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
		selfDesctructionScript = GetComponent<SelfDestruct>();
		gravitatingTowardsPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
        timerUntilDestruction -= Time.deltaTime;
		if(GameObject.FindGameObjectWithTag("Player") == true)
		{
			playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		}
	    if(transform.position.y >= startPosition.y + 3)
        {
			//Make the volicity change gradual. Slows down positive all the way until it gets to this value
            this.GetComponent<Rigidbody2D>().velocity = -Vector2.up;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;
            //this.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
		if(gravitatingTowardsPlayer)
		{
			moveToPlayer();
		}
        if(timerUntilDestruction <= 0)
        {
            selfDesctructionScript.obliteration();
        }
	}
}
