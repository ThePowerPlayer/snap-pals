using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject Enemy;
	private GameObject[] Enemies;
	private float[] spawnPosXArray = new float[] {-9.9f, 9.9f};
	private float[] spawnPosYArray = new float[] {-3.5f, -3.5f, -3.5f, 1f, 2f, 2f, 3f};
	private Vector2 spawnPos = new Vector2(0f, 0f);
	private float spawnDelay = 4f;
	private float spawnTimer = 4f;
	private float gameStartDelay = 5f;
	
	void Update()
	{
		if (GlobalVariables.lives <= 0)
		{
			Destroy(this);
		}
	}
	
    void FixedUpdate()
    {
		if (GlobalVariables.gameStart == true)
		{
			gameStartDelay -= Time.fixedDeltaTime;
			if (gameStartDelay <= 0f)
			{
				gameStartDelay = 0f;
			}
		}
		if (GlobalVariables.gameStart == true && gameStartDelay <= 0f)
		{
			spawnTimer -= Time.fixedDeltaTime;
			
			Enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (Enemies.Length == 0)
			{
				spawnTimer = 0f;
			}
			
			if (spawnTimer <= 0f)
			{
				spawnTimer = spawnDelay;
				spawnDelay -= 0.12f;
				
				spawnPos = new Vector2(spawnPosXArray[Random.Range(0, spawnPosXArray.Length)],
				spawnPosYArray[Random.Range(0, spawnPosYArray.Length)]);
				Instantiate(Enemy, spawnPos, Quaternion.identity);
			}
			spawnDelay = Mathf.Clamp(spawnDelay, 2f, 4f);
		}
    }
}
