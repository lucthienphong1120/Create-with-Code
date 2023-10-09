using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
	private Vector3 mouseStartPos;
	private float rotationSpeed = 10;
	
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			// Lấy vị trí chuột khi bắt đầu kéo
			mouseStartPos = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 mouseDelta = Input.mousePosition - mouseStartPos;

			transform.Rotate(Vector3.up, mouseDelta.x * rotationSpeed * Time.deltaTime);

			mouseStartPos = Input.mousePosition;
		}
	}
}

