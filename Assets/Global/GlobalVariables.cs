using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalVariables : MonoBehaviour
{
	public static int score;
	private string scoreString;
	Text scoreText;
	
	public static int lives;
	public static float invincibilityFrames = 0f;
	public static float invincibilityDuration = 3f;
	public static float invincibilityFade;
	private float invincibilityFadeChange;
	
    void Start()
    {
        score = 0;
		scoreText = GetComponent<Text>();
		lives = 3;
    }
	
	void Update()
	{
		scoreText.text = score.ToString();
	}
	
	void FixedUpdate()
	{
		// Give BOTH players invincibility frames if one of them was hit
		// (Keyword being both; that's why it needs to be a global variable)
		if (PlayerMovement.hurt == true)
		{
			PlayerMovement.hurt = false;
			lives -= 1;
			invincibilityFrames = invincibilityDuration;
		}
		
		// Subtract invincibility frames until their value reaches 0
		invincibilityFrames -= Time.fixedDeltaTime;
		if (invincibilityFrames <= 0f)
		{
			invincibilityFrames = 0f;
		}
		
		// Invincibility fade (1 = opaque, 0 = transparent)
		if (invincibilityFrames > 0f)
		{
			if (invincibilityFade <= 0.5f)
			{
				invincibilityFadeChange = Time.fixedDeltaTime * 2;
			}
			else if (invincibilityFade >= 1f)
			{
				invincibilityFadeChange = -(Time.fixedDeltaTime * 2);
			}
			invincibilityFade += invincibilityFadeChange;
		}
		else if (invincibilityFrames <= 0f)
		{
			invincibilityFade = 1;
			invincibilityFadeChange = Time.fixedDeltaTime * 2;
		}
	}
}
