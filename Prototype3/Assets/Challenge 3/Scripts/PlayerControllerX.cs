using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;
	private bool isLowEnough = true;

	private float floatForce = 120;
    private float gravityModifier = 1.5f;
    private float topBound = 16;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

	// Start is called before the first frame update
	void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
		playerRb = GetComponent<Rigidbody>();
		// Apply a small upward force at the start of the game
		playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
	{
        // Check if player fly to high, set isLowEnough to false
		if (transform.position.y >= topBound)
		{
			isLowEnough = false;
		} else
        {
            isLowEnough = true;
        }
		// While space is pressed and player is low enough, float up
		if (Input.GetKey(KeyCode.Space) && !gameOver & isLowEnough)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

		}
		// if player collides with ground, bounce off
		else if (other.gameObject.CompareTag("Ground"))
		{
			playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
		}

	}

}
