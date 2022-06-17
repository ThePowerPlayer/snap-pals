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
	
    void Start()
    {
        score = 0;
		scoreText = GetComponent<Text>();
    }
	
	void Update()
	{
		scoreText.text = score.ToString();
	}
}
