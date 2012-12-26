using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MoveAce : MonoBehaviour
{
	public float moveSpeed = 2f;

	const float maxX = 1.7f;

	void Update()
	{
		float dx = Input.GetAxisRaw("Horizontal");

		Vector3 pos = transform.position;

		pos.x = Mathf.Clamp(pos.x + dx * Time.deltaTime * moveSpeed, maxX * -1, maxX);

		transform.position = pos;

	}
}
