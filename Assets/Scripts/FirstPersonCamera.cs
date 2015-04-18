using UnityEngine;
using System.Collections;

public class FirstPersonCamera : MonoBehaviour
{
	public Vector3 Direction
	{
		get
		{
			float angle = transform.rotation.eulerAngles.x * Mathf.Deg2Rad;
			return new Vector3(0f, Mathf.Sin(-angle), Mathf.Cos(angle));

		}
	}

	void Update()
	{
		Quaternion rotation = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y"), Vector3.right);
		transform.rotation *= rotation;
	}
}
