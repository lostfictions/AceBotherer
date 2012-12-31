using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MoveBackdrop : MonoBehaviour
{
	public Transform target;
	public float moveScale = -10f;
	
	void Update()
	{
		Vector3 pos = transform.position;
		pos.x = target.position.x * moveScale;
		transform.position = pos;
	}
}
