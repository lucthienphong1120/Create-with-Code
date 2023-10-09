using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public GameObject player;
	// 3rd view
	private Vector3 view3rd = new Vector3(0, 10, -10);
	private Quaternion defaultRotate;
	// 1st view
	private Vector3 view1st = new Vector3(0, 4.5f, 0);
	private bool switchCamera;
	// Start is called before the first frame update
	void Start()
	{
		switchCamera = false;
		defaultRotate = transform.rotation;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			switchCamera = !switchCamera;
		}
		if (switchCamera)
		{
			transform.position = player.transform.position + view1st;
			transform.rotation = player.transform.rotation;
		}
		else
		{
			transform.position = player.transform.position + view3rd;
			transform.rotation = defaultRotate;
		}
	}
}
