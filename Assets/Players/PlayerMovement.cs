using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
	private PolygonCollider2D coll;
	
	private KeyCode LeftKey;
	private KeyCode RightKey;
	private KeyCode JumpKey;
	
	private const float speed = 1f;
	private const float topSpeed = 10f;
	private const float jumpHeight = 25f;
	private float friction = 0.5f;
	
	public static string onTop;
	
	[SerializeField] private LayerMask jumpableGround;
	
	private void XVelocitySetTo(float x)
	{
		rb.velocity = new Vector2(x, rb.velocity.y);
	}
	
	private void YVelocitySetTo(float y)
	{
		rb.velocity = new Vector2(rb.velocity.x, y);
	}
	
	private void XVelocityAdd(float x)
	{
		rb.velocity = new Vector2(rb.velocity.x + x, rb.velocity.y);
	}
	
	private bool IsGrounded()
	{
		return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
	}
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<PolygonCollider2D>();
		
		if (gameObject.name == "Blue" || gameObject.name == "Blue(Clone)")
		{
			gameObject.tag = "Blue";
			LeftKey = KeyCode.A;
			RightKey = KeyCode.D;
			JumpKey = KeyCode.W;
		}
		if (gameObject.name == "Pink" || gameObject.name == "Pink(Clone)")
		{
			gameObject.tag = "Pink";
			LeftKey = KeyCode.LeftArrow;
			RightKey = KeyCode.RightArrow;
			JumpKey = KeyCode.UpArrow;
		}
		
		onTop = "None";
    }

    void Update()
    {
		// Left and right movement
        if (Input.GetKey(LeftKey) && gameObject.tag != onTop)
		{
			XVelocityAdd(-(speed));
		}
		if (Input.GetKey(RightKey) && gameObject.tag != onTop)
		{
			XVelocityAdd(speed);
		}
		
		// Jumping
		if (Input.GetKeyDown(JumpKey))
		{
			if (onTop == "None")
			{
				if (IsGrounded())
				{
					YVelocitySetTo(jumpHeight);
				}
				else if (!IsGrounded())
				{
					YVelocitySetTo(rb.velocity.y / 2);
				}
			}
			else if (gameObject.tag == onTop)
			{
				Debug.Log("Jumped out of the connection!");
				onTop = "None";
				YVelocitySetTo(jumpHeight);
			}
		}
		
		// Generate artificial horizontal friction if not moving left or right
		// Change friction depending on whether in air or on ground
		if (IsGrounded())
		{
			friction = 0.5f;
		}
		else if (!IsGrounded())
		{
			friction = 0.3f;
		}
		// Left friction (to counter right velocity)
		if (rb.velocity.x > 0)
		{
			
			XVelocitySetTo(rb.velocity.x + (friction * -1f));
			if (rb.velocity.x < 0)
			{
				XVelocitySetTo(0);
			}
		}
		// Right friction (to counter left velocity)
		if (rb.velocity.x < 0)
		{
			
			XVelocitySetTo(rb.velocity.x + (friction * 1f));
			if (rb.velocity.x > 0)
			{
				XVelocitySetTo(0);
			}
		}
		
        // Cap max velocity
		XVelocitySetTo(Mathf.Clamp(rb.velocity.x, -(topSpeed), topSpeed));
		YVelocitySetTo(Mathf.Clamp(rb.velocity.y, -(jumpHeight), jumpHeight));
		
		// Make the player on top stick to the one on the bottom
		
		if (gameObject.tag == onTop)
		{
			if (gameObject.tag == "Blue")
			{
				GameObject Pink = GameObject.FindWithTag("Pink");
				transform.position = 
				new Vector2(Pink.transform.position.x, transform.position.y);
			}
			if (gameObject.tag == "Pink")
			{
				GameObject Blue = GameObject.FindWithTag("Blue");
				transform.position = 
				new Vector2(Blue.transform.position.x, transform.position.y);
			}
		}
		
    }
}
