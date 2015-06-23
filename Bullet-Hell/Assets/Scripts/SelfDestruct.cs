using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		Destroy(gameObject);
	}
}
