using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] enemyPrefabs;
	public GameObject[] powerupPrefabs;
	private float spawnRange = 9f;
	public int enemyCount;
	public int waveNumber;
	// Start is called before the first frame update
	void Start()
	{
		waveNumber = 0;
	}

	// Update is called once per frame
	void Update()
	{
		enemyCount = FindObjectsOfType<Enemy>().Length;
		if (enemyCount == 0)
		{
			waveNumber++;
			SpawnEnemyWave(waveNumber);
			SpawnPowerup();
		}
	}

	void SpawnEnemyWave(int enemiesToSpawn)
	{
		for (int i = 0; i < enemiesToSpawn; i++)
		{
			int randomEnemy = Random.Range(0, enemyPrefabs.Length);
			Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), Quaternion.identity);
		}
	}

	void SpawnPowerup()
	{
		int randomPowerup = Random.Range(0, powerupPrefabs.Length);
		Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), Quaternion.identity);
	}


	private Vector3 GenerateSpawnPosition()
	{
		float spawnPosX = Random.Range(-spawnRange, spawnRange);
		float spawnPosZ = Random.Range(-spawnRange, spawnRange);
		Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
		return randomPos;
	}
}
