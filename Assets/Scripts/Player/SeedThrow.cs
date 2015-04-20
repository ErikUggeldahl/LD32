using UnityEngine;
using System.Collections;

public class SeedThrow : MonoBehaviour
{
	[SerializeField]
	GameObject seedGO;

	[SerializeField]
	Transform throwPoint;

	[SerializeField]
	FirstPersonCamera cameraControl;

	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioClip throwSound;

	const float throwForce = 25f;

	const int minSeedCount = 3;
	const int maxSeedCount = 6;

	bool canThrow = false;

	public void EnableThrowing()
	{
		canThrow = true;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && canThrow)
			ThrowSeeds();
	}

	void ThrowSeeds()
	{
		audioSource.PlayOneShot(throwSound);

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
