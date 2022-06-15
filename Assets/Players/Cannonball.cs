using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
	private Rigidbody2D rb;
	private SpriteRenderer sRenderer;
	
	private float speed;
	private Vector2 fireDirec;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		sRenderer = GetComponent<SpriteRenderer>();
		
		if (PlayerMovement.onTop == "Blue")
		{
			transform.localScale = new Vector2(0.35f, 0.35f);
			sRenderer.sortingOrder = -1;
			speed = 5f;
			fireDirec = new Vector2(Cannon.cursorPos, 1f);
		}
		else if (PlayerMovement.onTop != "Blue")
		{
			transform.localScale = new Vector2(0.5f, 0.5f);
			sRenderer.sortingOrder = 1;
			speed = 10f;
			if (PlayerMovement.facingRight == false)
			{
				fireDirec = new Vector2(-1f, 0f);
			}
			else if (PlayerMovement.facingRight == true)
			{
				fireDirec = new Vector2(1f, 0f);
			}
		}
    }
	
    void Update()
    {
        // Out of bounds
		if (transform.position.x < -10f ||
		transform.position.x > 10f ||
		transform.position.y < -6f ||
		transform.position.y > 6f)
		{
			Destroy(gameObject);
		}
    }
	
	void FixedUpdate()
	{
		rb.MovePosition(rb.position + fireDirec * speed * Time.fixedDeltaTime);
	}
}
