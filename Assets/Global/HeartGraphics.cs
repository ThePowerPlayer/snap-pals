using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGraphics : MonoBehaviour
{
	public Sprite Heart1;
	public Sprite Heart2;
	public Sprite Heart3;
	private SpriteRenderer sRenderer;
	
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }
	
    void Update()
    {
		if (GlobalVariables.lives <= 0)
		{
			sRenderer.enabled = false;
		}
		else if (GlobalVariables.lives == 1)
		{
			sRenderer.enabled = true;
			sRenderer.sprite = Heart1;
		}
		else if (GlobalVariables.lives == 2)
		{
			sRenderer.enabled = true;
			sRenderer.sprite = Heart2;
		}
        else if (GlobalVariables.lives == 3)
		{
			sRenderer.enabled = true;
			sRenderer.sprite = Heart3;
		}
    }
}
