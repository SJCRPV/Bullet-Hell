using UnityEngine;
using System.Collections;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField]
    private float activeTime;

    private Collider2D circleCollider;
    private SpriteRenderer spriteRen;
    private float activeTimeStore;

    public void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("Collider's layer is: " + collider.gameObject.layer);
        if (collider.gameObject.layer == 11)
        {
            Destroy(collider.gameObject);
        }
    }

	// Use this for initalization
	private void Start()
	{
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        if(circleCollider == null)
        {
            Debug.LogError("Failed to get CircleCollider2D. circleCollider is null.");
        }
        spriteRen = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRen == null)
        {
            Debug.LogError("Failed to get SpriteRenderer. spriteRen is null.");
        }
        activeTimeStore = activeTime;
	}
	
	// Runs every frame
	private void Update()
	{
		if(circleCollider.enabled)
        {
            activeTime -= Time.deltaTime;
        }

        if(activeTime <= 0)
        {
            circleCollider.enabled = false;
            spriteRen.enabled = false;
            activeTime = activeTimeStore;
        }
	}
}