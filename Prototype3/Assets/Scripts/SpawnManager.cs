using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] obstaclePrefabs;
	private int index;
	private Vector3 spawnPos = new Vector3(25, 0, 0);
	private float startDelay = 2;
	private float repeatRate = 2;
	private PlayerController playerControllerScript;
	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
		playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void SpawnObstacle()
	{
		if (!playerControllerScript.gameOver)
		{
			index = Random.Range(0, obstaclePrefabs.Length);
			Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
		}
	}
}
