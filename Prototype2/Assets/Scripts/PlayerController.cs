using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private float horizontalInput;
    private float verticallInput;
	private float speed = 15;
    private float xRange = 15;
    private float zMin = -1;
    private float zMax = 15;

    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // limit the area player can move
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
		if (transform.position.x < -xRange)
		{
			transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
		}
		if (transform.position.z > zMax)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
		}
		if (transform.position.z < zMin)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, zMin);
		}
		// move the player
		horizontalInput = Input.GetAxis("Horizontal");
		verticallInput = Input.GetAxis("Vertical");
		transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
		transform.Translate(Vector3.forward * verticallInput * speed * Time.deltaTime);

		// player shoot projectile
		if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
