using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] animalPrefab;

	private float spawnRangeX = 15;
	private float spawnZMin = 4;
	private float spawnPosTop = 20;
	private float spawnPosRight = 24;
	private float spawnPosLeft = -20;
	private float startDelay = 2;
	private float spawnInterval = 3;
	private int animalIndex;
	private Vector3 spawnPos;
	private Quaternion spawnRotateDown = Quaternion.Euler(0, 180, 0);
	private Quaternion spawnRotateLeft = Quaternion.Euler(0, 270, 0);
	private Quaternion spawnRotateRight = Quaternion.Euler(0, 90, 0);
	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void SpawnRandomAnimal()
	{
		// spawn animal down
		animalIndex = Random.Range(0, animalPrefab.Length);
		spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosTop);
		Instantiate(animalPrefab[animalIndex], spawnPos, spawnRotateDown);
		// spawn animal left
		animalIndex = Random.Range(0, animalPrefab.Length);
		spawnPos = new Vector3(spawnPosRight, 0, Random.Range(spawnZMin, spawnRangeX));
		Instantiate(animalPrefab[animalIndex], spawnPos, spawnRotateLeft);
		// spawn animal right
		animalIndex = Random.Range(0, animalPrefab.Length);
		spawnPos = new Vector3(spawnPosLeft, 0, Random.Range(spawnZMin, spawnRangeX));
		Instantiate(animalPrefab[animalIndex], spawnPos, spawnRotateRight);
	}
}
