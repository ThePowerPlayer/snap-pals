using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGraphics : MonoBehaviour
{
	public Sprite StartText;
	public Sprite Heart1;
	public Sprite Heart2;
	public Sprite Heart3;
	public Sprite Restart;
	private SpriteRenderer sRenderer;
	
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }
	
    void Update()
    {
		if (GlobalVariables.gameStart == false)
		{
			sRenderer.sprite = StartText;
		}
		else if (GlobalVariables.gameStart == true)
		{
			if (GlobalVariables.lives <= 0)
			{
				sRenderer.sprite = Restart;
			}
			else if (GlobalVariables.lives == 1)
			{
				sRenderer.sprite = Heart1;
			}
			else if (GlobalVariables.lives == 2)
			{
				sRenderer.sprite = Heart2;
			}
			else if (GlobalVariables.lives == 3)
			{
				sRenderer.sprite = Heart3;
			}
		}
    }
}
