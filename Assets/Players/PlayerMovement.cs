using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
	private PolygonCollider2D coll;
	private SpriteRenderer sRenderer;
	
	public Sprite BlueStandard;
	public Sprite BlueOnBottom;
	
	private KeyCode LeftKey;
	private KeyCode RightKey;
	private KeyCode JumpKey;
	
	private const float speed = 1f;
	private const float topSpeed = 10f;
	private const float jumpHeight = 22f;
	private float friction = 0.5f;
	
	public static string onTop;
	public static bool facingRight;
	public static bool hurt;
	
	public GameObject Cannon;
	public GameObject DeathParticle;
	
	private AudioSource audioSource;
	
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
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		coll = GetComponent<PolygonCollider2D>();
		sRenderer = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
		
		if (gameObject.name == "Blue" || gameObject.name == "Blue(Clone)")
		{
			gameObject.tag = "Blue";
			LeftKey = KeyCode.LeftArrow;
			RightKey = KeyCode.RightArrow;
			JumpKey = KeyCode.UpArrow;
			Instantiate(Cannon,
			new Vector3(transform.position.x, transform.position.y + 0.75f,
			transform.position.z), Quaternion.identity);
		}
		if (gameObject.name == "Pink" || gameObject.name == "Pink(Clone)")
		{
			gameObject.tag = "Pink";
			LeftKey = KeyCode.A;
			RightKey = KeyCode.D;
			JumpKey = KeyCode.W;
		}
		
		onTop = "None";
		hurt = false;
    }

    void Update()
    {
		// Start game
		if (GlobalVariables.gameStart == true)
		{
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
		
		// Change Blue sprite
		if (gameObject.tag == "Blue")
		{
			if (onTop == "Pink")
			{
				sRenderer.sprite = BlueOnBottom;
			}
			else if (onTop != "Pink")
			{
				sRenderer.sprite = BlueStandard;
			}
		}
		
		// Left and right movement
        if (Input.GetKey(LeftKey) && gameObject.tag != onTop)
		{
			XVelocityAdd(-(speed));
			facingRight = false;
		}
		if (Input.GetKey(RightKey) && gameObject.tag != onTop)
		{
			XVelocityAdd(speed);
			facingRight = true;
		}
		
		// Jumping
		if (Input.GetKeyDown(JumpKey))
		{
			if (onTop == "None" && IsGrounded())
			{
				YVelocitySetTo(jumpHeight);
			}
			else if (gameObject.tag == onTop)
			{
				onTop = "None";
				YVelocitySetTo(jumpHeight);
			}
		}
		if (Input.GetKeyUp(JumpKey) && !IsGrounded())
		{
			YVelocitySetTo(rb.velocity.y / 2);
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
		
		// Game over!
		if (GlobalVariables.lives <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Instantiate(DeathParticle, transform.position, Quaternion.identity);
			}
			Destroy(gameObject);
		}
    }
	
	void FixedUpdate()
	{
		// Slightly fade sprite during invincibility
		sRenderer.color = new Color(1, 1, 1, GlobalVariables.invincibilityFade);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy" && GlobalVariables.invincibilityFrames == 0f)
		{
			if (GlobalVariables.lives >= 2)
			{
				audioSource.Play();
			}
			hurt = true;
		}
	}
}
