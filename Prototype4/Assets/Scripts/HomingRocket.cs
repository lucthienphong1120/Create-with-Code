using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
	private Transform target;
	private float speed = 15;
	private float rocketStrength = 10;
	private float aliveTimer = 3;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (target != null)
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (target != null)
		{
			if(collision.gameObject.CompareTag("Enemy"))
			{
				Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
				Vector3 awayDirection = -collision.GetContact(0).normal;
				enemyRb.AddForce(awayDirection * rocketStrength, ForceMode.Impulse);
				Destroy(gameObject);
			}
		}
	}
	public void Fire(Transform homingTarget)
	{
		target = homingTarget;
		transform.LookAt(target);
		Destroy(gameObject, aliveTimer);
	}

}
