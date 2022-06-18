using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
	private SpriteRenderer sRenderer;
	private Color fade;
	private Rigidbody2D rb;
	
    void Start()
    {
		sRenderer = GetComponent<SpriteRenderer>();
		fade = sRenderer.color;
        rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
    }
	
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x * 0.99f, rb.velocity.y * 0.99f);
    }
	
	void FixedUpdate()
	{
		fade.a -= 0.01f;
		sRenderer.color = fade;
		if (sRenderer.color.a <= 0f)
		{
			Destroy(gameObject);
		}
	}
}
