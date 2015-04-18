using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour
{
	[SerializeField]
	GameObject shootGO;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
			return;

		Instantiate(shootGO, collision.contacts[0].point, Quaternion.identity);

		Destroy(gameObject);
	}
}
