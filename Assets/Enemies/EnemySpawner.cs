using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject Enemy;
	private float[] spawnPosXArray = new float[] {-9.9f, 9.9f};
	private float[] spawnPosYArray = new float[] {-3.5f, -3.5f, -3.5f, 2f, 3f, 4f};
	private Vector2 spawnPos = new Vector2(0f, 0f);
	private float spawnDelay = 5f;
	private float spawnTimer = 5f;
	
    void FixedUpdate()
    {
		spawnTimer -= Time.fixedDeltaTime;
		if (spawnTimer <= 0f)
		{
			spawnTimer = spawnDelay;
			spawnDelay -= 0.04f;
			
			spawnPos = new Vector2(spawnPosXArray[Random.Range(0, spawnPosXArray.Length)],
			spawnPosYArray[Random.Range(0, spawnPosYArray.Length)]);
			Instantiate(Enemy, spawnPos, Quaternion.identity);
		}
		spawnDelay = Mathf.Clamp(spawnDelay, 2f, 5f);
    }
}
