using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
	private GameManager gameManager;
	private Vector3 mousePos;
	private TrailRenderer trail;
	private BoxCollider col;
	private bool swiping = false;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (gameManager.isGameActive)
		{
			if (Input.GetMouseButtonDown(0))
			{
				swiping = true;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				swiping = false;
			}
			UpdateComponents(swiping);
			if (swiping)
			{
				UpdateMousePosition();
			}
		}
	}

	void Awake()
	{
		trail = GetComponent<TrailRenderer>();
		col = GetComponent<BoxCollider>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		trail.enabled = false;
		col.enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Target>())
		{
			//Destroy the target
			other.gameObject.GetComponent<Target>().DestroyTarget();
		}
	}

	void UpdateMousePosition()
	{
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
		transform.position = mousePos;
	}

	void UpdateComponents(bool status)
	{
		trail.enabled = status;
		col.enabled = status;
	}



}
