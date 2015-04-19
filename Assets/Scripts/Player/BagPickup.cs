using UnityEngine;
using System.Collections;

public class BagPickup : MonoBehaviour
{
	[SerializeField]
	AudioClip pickupSound;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		other.GetComponent<SeedThrow>().EnableThrowing();
		other.GetComponent<AudioSource>().PlayOneShot(pickupSound);

		Destroy(gameObject);

	}
}
