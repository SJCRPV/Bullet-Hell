using UnityEngine;
using System.Collections;

public class StopMoving : MonoBehaviour {

    Vector3 startPosition;

    public float gravityScale;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.position.y >= startPosition.y + 3)
        {
			//Make the volicity change gradual
            this.GetComponent<Rigidbody2D>().velocity = -Vector2.up;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;
            //this.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
	}
}
