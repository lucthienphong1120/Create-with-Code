using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
	private Rigidbody targetRb;
	private GameManager gameManager;
	public ParticleSystem explosionParticle;
	private float minSpeed = 10;
	private float maxSpeed = 15;
	private float maxTorque = 10;
	private float xRange = 4;
	private float ySpawnPos = -2;
	public int pointValue = 5;
	// Start is called before the first frame update
	void Start()
	{
		targetRb = GetComponent<Rigidbody>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		targetRb.AddForce(RandomForce(), ForceMode.Impulse);
		targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
		transform.position = RandomSpawnPos();
	}

	// Update is called once per frame
	void Update()
	{

	}	

	private void OnMouseDown()
	{
		DestroyTarget();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (gameObject.CompareTag("Good") && other.CompareTag("Sensor"))
		{
			Destroy(gameObject);
			gameManager.UpdateLives(-1);
		}
	}
	public void DestroyTarget()
	{
		if (gameManager.isGameActive)
		{
			gameManager.UpdateScore(pointValue);
			Instantiate(explosionParticle, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	Vector3 RandomForce()
	{
		return Vector3.up * Random.Range(minSpeed, maxSpeed);
	}

	float RandomTorque()
	{
		return Random.Range(-maxTorque, maxTorque);
	}

	Vector3 RandomSpawnPos()
	{
		return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
	}
}
