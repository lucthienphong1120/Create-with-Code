using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
	private float topBound = 30;
	private float lowerBound = -10;
	private GameManager gameManager;
	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{
		// check if projectile go pass the screen, remove it
		if (transform.position.z > topBound)
		{
			Destroy(gameObject);
			return;
		}
		// check if an object go pass the player, remove it and game over
		else if (transform.position.z < lowerBound)
		{
			gameManager.AddLives(-1);
			Destroy(gameObject);
			return;
		}
	}
}
