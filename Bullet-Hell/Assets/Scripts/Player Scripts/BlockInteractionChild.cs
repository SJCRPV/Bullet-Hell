using UnityEngine;
using System.Collections;

public class BlockInteractionChild : MonoBehaviour {

	BlockInteraction blockInteractionScript;

	// Use this for initialization
	void Start () {
		blockInteractionScript = GetComponentInParent<BlockInteraction>();
	}
}
