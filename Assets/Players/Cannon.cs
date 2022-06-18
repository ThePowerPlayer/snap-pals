using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	private SpriteRenderer sRenderer;
	private Color fade;
	
	private GameObject Blue;
	public GameObject Cannonball;
	private Vector2 cannonballPos;
	
	public static float cursorPos = 0f;
	private float fireDelay;
	private float fireTime = 0f;
	
	private AudioSource audioSource;
	
    void Start()
    {
		sRenderer = GetComponent<SpriteRenderer>();
		sRenderer.enabled = false;
		fade = sRenderer.color;
		Blue = GameObject.FindWithTag("Blue");
		audioSource = GetComponent<AudioSource>();
    }
	
    void Update()
    {
		transform.position = 
		new Vector2(Blue.transform.position.x, Blue.transform.position.y + 0.75f);
		
		// Update whether cannonballs spawn from cannon or center (when Pink's on top)
		// Update duration of delay in between shots
		if (PlayerMovement.onTop == "Blue")
		{
			sRenderer.enabled = true;
			cannonballPos = 
			new Vector2(transform.position.x + (cursorPos / 5f), transform.position.y + 0.2f);
			fireDelay = 0.4f;
		}
		else if (PlayerMovement.onTop != "Blue")
		{
			sRenderer.enabled = false;
			cannonballPos = 
			new Vector2(transform.position.x, transform.position.y - 0.35f);
			fireDelay = 0.6f;
		}
		
		// Cursor position (relative to player) and cannon rotation
		cursorPos = ((Input.mousePosition.x / Screen.width) -
		(CameraVariables.blueScreenPos / Screen.width)) * 4f;
		cursorPos = Mathf.Clamp(cursorPos, -1f, 1f);
		transform.eulerAngles = 
		new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 40f * -(cursorPos));
		
		// Firing cannon
		if (((PlayerMovement.onTop == "Blue" && Input.GetMouseButton(0)) ||
		(PlayerMovement.onTop == "Pink" && Input.GetKey(KeyCode.Space)))
		&& fireTime <= 0f)
		{
			audioSource.pitch = Random.Range(0.8f, 1.2f);
			audioSource.Play();
			fireTime = fireDelay;
			Instantiate(Cannonball, cannonballPos, Quaternion.identity);
		}
		
		// Game over!
		if (GlobalVariables.lives <= 0)
		{
			Destroy(gameObject);
		}
    }
	
	void FixedUpdate()
	{
		fireTime -= Time.fixedDeltaTime;
		if (fireTime <= 0f)
		{
			fireTime = 0f;
		}
		
		// Slightly fade sprite during invincibility
		fade.a = GlobalVariables.invincibilityFade;
		sRenderer.color = fade;
	}
}
