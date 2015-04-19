﻿using UnityEngine;
using System.Collections;

public class SeedThrow : MonoBehaviour
{
	[SerializeField]
	GameObject seedGO;

	[SerializeField]
	Transform throwPoint;

	[SerializeField]
	FirstPersonCamera cameraControl;

	const float throwForce = 20f;

	const int minSeedCount = 3;
	const int maxSeedCount = 6;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			ThrowSeeds();
	}

	void ThrowSeeds()
	{
		for (int i = 0; i < Random.Range(minSeedCount, maxSeedCount); i++)
			ThrowSeed();
	}

	void ThrowSeed()
	{
		var seed = Instantiate(seedGO, throwPoint.position, Random.rotation) as GameObject;

		var throwRandom = Random.insideUnitSphere * 0.1f;
		seed.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(cameraControl.Direction + throwRandom) * throwForce, ForceMode.Impulse);
	}
}