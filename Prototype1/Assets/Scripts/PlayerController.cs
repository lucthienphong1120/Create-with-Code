using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRb;
	public GameObject centerOfMass;
	public Text speedometerText;
	public List<WheelCollider> allWheels;
	private int wheelsOnGround;
	private float speed;
	public float horsePower = 15000;
	private float turnSpeed = 25.0f;
	private float horizontalInput;
	private float verticalInput;

	// Start is called before the first frame update
	void Start()
	{
		playerRb = GetComponent<Rigidbody>();
		playerRb.centerOfMass = centerOfMass.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (IsOnGround())
		{
			// Get user inputs
			horizontalInput = Input.GetAxis("Horizontal");
			verticalInput = Input.GetAxis("Vertical");
			// Move vehicle forward
			playerRb.AddRelativeForce(Vector3.forward * verticalInput * horsePower);
			speed = Mathf.Round(playerRb.velocity.magnitude * 3.6f);
			speedometerText.text = "Speed: " + speed + "km/h";
			// Turn vehicle around up axis
			transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
		}
	}

	bool IsOnGround()
	{
		wheelsOnGround = 0;
		foreach (WheelCollider wheel in allWheels)
		{
			if (wheel.isGrounded)
			{
				wheelsOnGround++;
			}
		}
		if (wheelsOnGround == 4)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
