using UnityEngine;
using System.Collections;

public class PandaDeathSound : MonoBehaviour
{
	[SerializeField]
	AudioClip pandaDeathSound;

	public void Play()
	{
		GetComponent<AudioSource>().PlayOneShot(pandaDeathSound);
	}
}
