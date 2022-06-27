using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Rigidbody2D rb;
	private BoxCollider2D coll;
	
	private SpriteRenderer sRenderer;
	private Color Grounded = new Color(1f, 0.25f, 0.25f, 1f);
	private Color Flying = new Color(1f, 0.5f, 0.5f, 1f);
	private Color Swooping = new Color(1f, 1f, 0.5f, 1f);
	
	private Vector3 movement = new Vector3(0f, 0f, 0f);
	private float speed = 1f;
	
	private bool dead = false;
	private float[] fallDirecs = new float[] {-1f, 1f};
	private float fallDirec;
	
	private int swoopChance;
	private float swoopTimer;
	private float swoopSpeed = 0f;
	
	private AudioSource audioSource;
	
	private void Die()
	{
		dead = true;
		if (GetComponent<BoxCollider2D>() != null) coll.enabled = false;
		fallDirec = fallDirecs[Random.Range(0, fallDirecs.Length)];
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.gravityScale = 4f;
	}
	
    void Start()
    {
        if (GetComponent<Rigidbody2D>() != null) rb = GetComponent<Rigidbody2D>();
		if (GetComponent<BoxCollider2D>() != null) coll = GetComponent<BoxCollider2D>();
		if (GetComponent<SpriteRenderer>() != null) sRenderer = GetComponent<SpriteRenderer>();
		
		if (transform.position.y <= -3.5f)
		{
			sRenderer.color = Grounded;
		}
		else if (transform.position.y > -3.5f)
		{
			swoopChance = Random.Range(1, 101);
			if (swoopChance <= 40)
			{
				sRenderer.color = Swooping;
				swoopTimer = Random.Range(5f, 8f);
			}
			else if (swoopChance > 40)
			{
				sRenderer.color = Flying;
			}
		}
		
		if (transform.position.x < 0f)
		{
			movement = new Vector3(1f, 0f, 0f);
		}
		else if (transform.position.x > 0f)
		{
			movement = new Vector3(-1f, 0f, 0f);
		}
		
		audioSource = GetComponent<AudioSource>();
    }
	
	void Update()
	{
		if (GlobalVariables.lives <= 0 && !dead)
		{
			Die();
		}
		
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
		if (!dead)
		{
			if (sRenderer.color == Swooping)
			{
				swoopTimer -= Time.fixedDeltaTime;
				if (swoopTimer <= 0f)
				{
					swoopTimer = 0f;
					if (transform.position.y > -2f)
					{
						swoopSpeed += (Time.fixedDeltaTime / 3);
					}
					else if (transform.position.y <= -2f)
					{
						swoopSpeed = 0f;
					}
					swoopSpeed = Mathf.Clamp(swoopSpeed, 0f, 1f);
					movement[1] = -(swoopSpeed);
				}
			}
			rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
		}
		else if (dead)
		{
			transform.eulerAngles = 
			new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
			transform.eulerAngles.z + (fallDirec * 3f));
		}
    }
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag != "Blue" && col.gameObject.tag != "Pink")
		{
			audioSource.pitch = Random.Range(0.8f, 1.2f);
			audioSource.Play();
			GlobalVariables.score += 1;
		}
		if (!dead)
		{
			Die();
		}
	}
}
