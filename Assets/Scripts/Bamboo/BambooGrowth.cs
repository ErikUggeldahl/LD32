﻿using UnityEngine;
using System.Collections;

public class BambooGrowth : MonoBehaviour
{
	[SerializeField]
	Transform nextAnchor;
	public Transform NextAnchor { get { return nextAnchor; } }

	[SerializeField]
	GameObject segmentGO;

	[SerializeField]
	MeshFilter meshFilter;

	[SerializeField]
	Mesh segmentMesh;

	[SerializeField]
	Mesh topSegmentMesh;

	[SerializeField]
	MeshFilter meshFilterLOD1;

	[SerializeField]
	Mesh segmentMeshLOD1;

	[SerializeField]
	Mesh topSegmentMeshLOD1;

	[SerializeField]
	MeshFilter meshFilterLOD2;

	[SerializeField]
	Mesh segmentMeshLOD2;

	[SerializeField]
	Mesh topSegmentMeshLOD2;

	const float startYScale = 0.1f;
	const float growthTimeSec = 0.01f;
	const float growthRate = 1f / growthTimeSec;

	public int Index { get;  set; }

	public GrowStalk Stalk { get; set; }

	bool canPierce = false;

	public void StartGrowth()
	{
		transform.localScale = new Vector3(1f, startYScale, 1f);

		StartCoroutine(Grow());
	}

	IEnumerator Grow()
	{
		canPierce = true;

		SetMeshToTop();

		while (transform.localScale.y < 1)
		{
			var scaleRemaining = 1f - transform.localScale.y;
			var growth = Mathf.Clamp(growthRate * Time.deltaTime, 0f, scaleRemaining);

			transform.localScale += new Vector3(0f, growthRate, 0f) * Time.deltaTime;
			yield return null;
		}

		transform.localScale = new Vector3(1f, 1f, 1f);

		if (Index < Stalk.SectionCount)
			GrowNewSegment();
	}

	void GrowNewSegment()
	{
		canPierce = false;

		SetMeshToSegment();

		var nextSegment = Instantiate(segmentGO, nextAnchor.position, transform.rotation * Stalk.Lean) as GameObject;

		nextSegment.name = "Segment" + (Index + 1);
		nextSegment.transform.parent = transform;

		var growth = nextSegment.GetComponent<BambooGrowth>();
		growth.Index = Index + 1;
		growth.Stalk = Stalk;

		growth.StartGrowth();
	}

	void SetMeshToTop()
	{
		meshFilter.mesh = topSegmentMesh;
		meshFilterLOD1.mesh = topSegmentMeshLOD1;
	}

	void SetMeshToSegment()
	{
		meshFilter.mesh = segmentMesh;
		meshFilterLOD1.mesh = segmentMeshLOD1;
	}

	//void OnCollisionEnter(Collision collision)
	//{
	//	if (!canPierce)
	//		return;

	//	var collidedGO = collision.gameObject;
		
	//	if (collidedGO.layer == LayerMask.NameToLayer("Enemy"))
	//	{
	//		var joint = collidedGO.AddComponent<FixedJoint>();
	//		joint.connectedBody = GetComponent<Rigidbody>();

	//		collidedGO.GetComponent<Rigidbody>().isKinematic = true;

	//		collidedGO.layer = LayerMask.NameToLayer("DeadEnemy");
	//	}
	//}
}
