using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRb;
	private Animator playerAnim;
	private AudioSource playerAudio;
	public ParticleSystem explosionParticle;
	public ParticleSystem dirtParticle;
	public AudioClip jumpSound;
	public AudioClip crashSound;
	private float jumpForce = 500;
	private float gravityModifier = 1.5f;
	private bool isOnGround = true;
	private bool doubleJump = false;
	public bool doubleSpeed = false;
	public bool gameOver = false;
	// Start is called before the first frame update
	void Start()
	{
		playerRb = gameObject.GetComponent<Rigidbody>();
		playerAnim = gameObject.GetComponent<Animator>();
		playerAudio = gameObject.GetComponent<AudioSource>();
		Physics.gravity *= gravityModifier;
	}

	// Update is called once per frame
	void Update()
	{
		// handle double jump
		if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround && !gameOver)
		{
			Jump();
			doubleJump = false;
		} else if (Input.GetKeyDown(KeyCode.UpArrow) && !isOnGround && !doubleJump )
		{
			doubleJump = true;
			Jump();
		}
		// handle double speed
		if (Input.GetKey(KeyCode.RightArrow))
		{
			doubleSpeed = true;
			playerAnim.SetFloat("Speed_Multiplier", 1f);
		}
		else if (doubleSpeed)
		{
			doubleSpeed = false;
			playerAnim.SetFloat("Speed_Multiplier", 2f);
		}

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isOnGround = true;
			dirtParticle.Play();
		}
		else if (collision.gameObject.CompareTag("Obstacle"))
		{
			Debug.Log("Game Over");
			gameOver = true;
			playerAnim.SetBool("Death_b", true);
			playerAnim.SetInteger("DeathType_int", 1);
			explosionParticle.Play();
			dirtParticle.Stop();
			playerAudio.PlayOneShot(crashSound);
		}
	}

	private void Jump()
	{
		playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		isOnGround = false;
		playerAnim.SetTrigger("Jump_trig");
		dirtParticle.Stop();
		playerAudio.PlayOneShot(jumpSound);
	}
}
