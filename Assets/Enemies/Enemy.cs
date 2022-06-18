using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Rigidbody2D rb;
	private BoxCollider2D coll;
	private Vector3 movement = new Vector3(0f, 0f, 0f);
	private float speed = 1f;
	private bool dead = false;
	private float[] fallDirecs = new float[] {-1f, 1f};
	private float fallDirec;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
		if (transform.position.x < 0f)
		{
			movement = new Vector3(1f, 0f, 0f);
		}
		else if (transform.position.x > 0f)
		{
			movement = new Vector3(-1f, 0f, 0f);
		}
    }
	
	void Update()
	{
		if (GlobalVariables.lives <= 0)
		{
			dead = true;
			coll.enabled = false;
			fallDirec = fallDirecs[Random.Range(0, fallDirecs.Length)];
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.gravityScale = 4f;
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
			GlobalVariables.score += 1;
		}
		dead = true;
		coll.enabled = false;
		fallDirec = fallDirecs[Random.Range(0, fallDirecs.Length)];
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.gravityScale = 4f;
	}
}
