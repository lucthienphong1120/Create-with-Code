using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Rigidbody enemyRb;
	private GameObject player;
	public float speed = 3.5f;
	// Start is called before the first frame update
	void Start()
	{
		enemyRb = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 lookDirection = (player.transform.position - transform.position).normalized;
		enemyRb.AddForce(lookDirection * speed);

		if (transform.position.y < -10)
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
			Vector3 awayDirection = (transform.position - collision.gameObject.transform.position);
			playerRb.AddForce(awayDirection, ForceMode.Impulse);
		}
	}
}
