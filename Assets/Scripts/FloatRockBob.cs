using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class FloatRockBob : MonoBehaviour
{
	float initY;
	float period;

	void Start()
	{
		initY = transform.position.y;

		period = Random.Range(2f, 4f);
	}
	
	void Update()
	{
		Vector3 pos = transform.position;
		pos.y = initY + Mathy.Sin(0.23f, period, 0);
		transform.position = pos;
	}
}
