using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 5f;
	Renderer playerRend;
	static Ballistics fireScript;

	void Start()
	{
		fireScript = gameObject.GetComponent<Ballistics>();
		playerRend = GetComponent<SpriteRenderer>();
	}

	void Movement()
	{
		Vector3 pos = transform.position;

		//If you press shift, the speed halves
		if(Input.GetButton("Show Hitbox"))
		{
			pos.y += Input.GetAxis("Vertical") * (speed/2);
			pos.x += Input.GetAxis("Horizontal") * (speed/2);
		}
		else
		{
			pos.y += Input.GetAxis("Vertical") * speed;
			pos.x += Input.GetAxis("Horizontal") * speed;
		}

		//Top edge
		if(pos.y >= Camera.main.orthographicSize - playerRend.bounds.size.y/2)
		{
			pos.y = Camera.main.orthographicSize - playerRend.bounds.size.y/2;
		}

		//Bottom edge
		if(pos.y <= -(Camera.main.orthographicSize - playerRend.bounds.size.y/2))
		{
			pos.y = - (Camera.main.orthographicSize - playerRend.bounds.size.y/2);
		}
		
		float screenWidth = (float)Screen.width / (float)Screen.height;
		float screenX = screenWidth * Camera.main.orthographicSize;

		//Right edge
		if(pos.x >= screenX - playerRend.bounds.size.x/2)
		{
			pos.x = screenX - playerRend.bounds.size.x/2;
		}

		//Left edge
		if(pos.x <= -(screenX - playerRend.bounds.size.x/2))
		{
			pos.x = -(screenX - playerRend.bounds.size.x/2);
		}

		transform.position = pos;

	}

	void FireAtWill()
	{
		//Debug.Log("You rang?");
		if(Input.GetButton("Fire1"))
		{
			//Debug.Log("Fired");
			fireScript.Fire();
		}
	}

	// Update is called once per frame
	void Update () {
		Movement();
		FireAtWill();
	}
}
