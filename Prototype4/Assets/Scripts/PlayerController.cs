using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Powerup;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRb;
	private GameObject focalPoint;
	public GameObject powerupIndicator;
	public ParticleSystem smokeParticle;
	private Coroutine hasPowerup;
	private float powerupStrength = 12;
	private float speed = 5;
	private float jumpForce = 5;
	private float explosionForce = 100;
	private float explosionRadius = 8f;
	private float numberOfMissle = 3;
	public PowerUpType currentPowerUp = PowerUpType.None;
	public GameObject misslePrefab;
	private GameObject missle;
	// Start is called before the first frame update
	void Start()
	{
		playerRb = GetComponent<Rigidbody>();
		focalPoint = GameObject.Find("Focal Point");
	}

	// Update is called once per frame
	void Update()
	{
		float forwardInput = Input.GetAxis("Vertical");
		float rightInput = Input.GetAxis("Horizontal");
		playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
		playerRb.AddForce(focalPoint.transform.right * speed * rightInput);
		powerupIndicator.transform.position = transform.position;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Powerup"))
		{
			currentPowerUp = other.gameObject.GetComponent<Powerup>().powerUpType;
			powerupIndicator.SetActive(true);
			Destroy(other.gameObject);
			if (currentPowerUp == PowerUpType.Rockets)
			{
				LaunchRockets();
			}
			else if (currentPowerUp == PowerUpType.Shockware)
			{
				LaunchShockware();
			}
			// check if it's already powerup, stop it
			if (hasPowerup != null)
			{
				StopCoroutine(hasPowerup);
			}
			hasPowerup = StartCoroutine(PowerupCountdownRoutine());
		}
	}

	IEnumerator PowerupCountdownRoutine()
	{
		yield return new WaitForSeconds(5);
		currentPowerUp = PowerUpType.None;
		powerupIndicator.SetActive(false);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
			Vector3 awayDirection = (collision.gameObject.transform.position - transform.position);
			if (currentPowerUp == PowerUpType.Pushback)
			{
				enemyRb.AddForce(awayDirection * powerupStrength, ForceMode.Impulse);
			}
			else
			{
				enemyRb.AddForce(awayDirection, ForceMode.Impulse);
			}
		}
	}

	void LaunchRockets()
	{
		smokeParticle.Play();
		for (int i = 0; i < numberOfMissle; i++)
		{
			foreach (var enemy in FindObjectsOfType<Enemy>())
			{
				// launch the missiles from above the player, avoid collision from pushing us back
				missle = Instantiate(misslePrefab, transform.position + Vector3.up, Quaternion.identity);
				missle.GetComponent<HomingRocket>().Fire(enemy.transform);
			}
		}
	}

	void LaunchShockware()
	{
		smokeParticle.Play();
		var enemies = FindObjectsOfType<Enemy>();
		// push the player up
		playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		//Cycle through all enemies.
		while (true)
		{
			if (transform.position.y <= 0)
			{
				break;
			}
		}
		for (int i = 0; i < enemies.Length; i++)
		{
			//Apply an explosion force that originates from our position.
			if (enemies[i] != null)
			{
				enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
				transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
			}
		}
	}

}
