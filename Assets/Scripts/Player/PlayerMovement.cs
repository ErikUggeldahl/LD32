﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	float maxSpeed = 12f;
	float moveForce = 40f;

	const float jumpForce = 20f;

	bool canJump = false;
	bool isJumpReady = true;
	float jumpCooldown = 0.2f;

	[SerializeField]
	AudioClip jumpSound;
	[SerializeField]
	AudioClip landSound;
	[SerializeField]
	AudioClip joinGame;
	[SerializeField]
	AudioClip victorySound;


	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		GetComponent<AudioSource>().PlayOneShot(joinGame);

		ScoreCount.Instance.OnVictory += PlayVictorySound;
	}

	void Update()
	{
		Turn();
	}

	void FixedUpdate()
	{
		Run();
		Jump();
	}

	void Run()
	{
		Vector3 movement = Vector3.zero;
		movement.z += Input.GetAxis("Vertical");
		movement.x += Input.GetAxis("Horizontal");
		movement.Normalize();

		movement *= moveForce * Time.fixedDeltaTime;

		movement = transform.TransformDirection(movement);
		if (GetComponent<Rigidbody>().velocity.magnitude + movement.magnitude > maxSpeed)
			return;

		GetComponent<Rigidbody>().AddForce(movement, ForceMode.VelocityChange);
	}

	void Jump()
	{
		if (canJump && isJumpReady && Input.GetKey(KeyCode.Space))
		{
			GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
			StartCoroutine("JumpCooldown");
			GetComponent<AudioSource>().PlayOneShot(jumpSound);
		}
	}

	IEnumerator JumpCooldown()
	{
		isJumpReady = false;
		yield return new WaitForSeconds(jumpCooldown);
		isJumpReady = true;
	}

	public void JumpEnter()
	{
		canJump = true;

		GetComponent<AudioSource>().PlayOneShot(landSound);
	}

	public void JumpExit()
	{
		canJump = false;
	}

	void Turn()
	{
		transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
	}

	void PlayVictorySound()
	{
		GetComponent<AudioSource>().PlayOneShot(victorySound);
	}
}
