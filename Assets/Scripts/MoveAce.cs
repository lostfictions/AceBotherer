using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MoveAce : MonoBehaviour
{
	public float moveSpeed = 2f;
	public float maxX = 1.7f;

	public Transform endSkull;

	public float SteeringForce { get; private set; }

	void Update()
	{
		Vector3 pos = transform.position;

		if(pos.z > endSkull.position.z - 5f)
		{
			EndSteering();
			return;
		}

		float dx = Input.GetAxis("Horizontal");

		pos.x = Mathf.Clamp(pos.x + dx * Time.deltaTime * moveSpeed, maxX * -1, maxX);
		transform.position = pos;

		SteeringForce = dx;
	}

	void EndSteering()
	{
		Vector3 pos = transform.position;
		Vector3 targetpos = endSkull.position + Vector3.right * 0.1f;

		float dx = (targetpos.x - pos.x).Clamp(-1f, 1f);

		pos.x = Mathf.Clamp(pos.x + dx * Time.deltaTime * moveSpeed, maxX * -1, maxX);
		transform.position = pos;

		SteeringForce = dx;
	}
}
