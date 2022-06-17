using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Rigidbody2D rb;
	private Vector3 movement = new Vector3(0f, 0f, 0f);
	private float speed = 1f;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }
	
	void OnTriggerEnter2D()
	{
		GlobalVariables.score += 1;
		Destroy(gameObject);
	}
}
