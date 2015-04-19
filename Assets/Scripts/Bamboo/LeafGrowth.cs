using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeafGrowth : MonoBehaviour
{
	const float growthChance = 0.05f;

	[SerializeField]
	GameObject[] leafModels;

	[SerializeField]
	Transform[] leafPoints;

	[SerializeField]
	Transform leafParent;

	void Start()
	{
		ClearExistingLeaves();

		foreach (var point in leafPoints)
		{
			if (Random.value < growthChance)
				GrowLeaf(point);

		}
	}

	void ClearExistingLeaves()
	{
		var children = new List<GameObject>();
		foreach (Transform child in leafParent)
			children.Add(child.gameObject);

		children.ForEach(child => Destroy(child));
	}

	void GrowLeaf(Transform point)
	{
		var leafModel = leafModels[Random.Range(0, leafModels.Length)];

		var leaf = Instantiate(leafModel, point.position, Quaternion.LookRotation(point.forward)) as GameObject;
		leaf.transform.parent = leafParent;
	}
}
