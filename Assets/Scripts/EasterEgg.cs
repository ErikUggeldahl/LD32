using UnityEngine;
using System.Collections;

public class EasterEgg : MonoBehaviour
{
	[SerializeField]
	Transform player;

	[SerializeField]
	GameObject toEnable;

	[SerializeField]
	AudioClip easterEggSound;

	void Update()
	{
		if (player.position.y < -20f)
		{
			toEnable.SetActive(true);

			player.GetComponent<AudioSource>().PlayOneShot(easterEggSound);

			Destroy(this);
		}
	}
}
