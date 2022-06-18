using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
	private Rigidbody2D rb;
	private float velocityChange;
	private AudioSource audioSource;
	private bool soundPlayed;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		velocityChange = 1f;
    }
	
    void Update()
    {
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) &&
		soundPlayed == false)
		{
			audioSource.Play();
			soundPlayed = true;
		}
		
        // Out of bounds
		if (transform.position.y > 15f)
		{
			Destroy(gameObject);
		}
    }
	
	void FixedUpdate()
	{
		if (GlobalVariables.gameStart == true)
		{
			rb.velocity = new Vector2(0f, rb.velocity.y + (15f * Time.fixedDeltaTime));
		}
		else if (GlobalVariables.gameStart == false)
		{
			if (rb.velocity.y <= -0.5f)
			{
				velocityChange = 1f;
			}
			else if (rb.velocity.y >= 0.5f)
			{
				velocityChange = -1f;
			}
			rb.velocity = new Vector2(0f, rb.velocity.y + velocityChange * Time.fixedDeltaTime);
		}
	}
}
