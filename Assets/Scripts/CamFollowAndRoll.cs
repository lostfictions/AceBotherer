using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CamFollowAndRoll : MonoBehaviour
{
	public Transform target;

	public int sampleCount = 20;
	public AnimationCurve sampleWeights;

	public float maxPan = 1.7f;

	float[] positionSamples;
	int indPositionSamples = 0;

	float lastX;

	void Start()
	{
		positionSamples = new float[sampleCount];

		float x = target.position.x;
		for(int i=0; i<sampleCount; i++)
		{
			positionSamples[i] = x;
		}

		lastX = transform.position.x;
	}

	void Update()
	{
		float weightedSum = 0;
		float sumOfWeights = 0; //hee hee i am best namer //also we don't need to calculate this every frame, wtvs

		for(int i=0; i<sampleCount; i++)
		{
			int indWrapped = (indPositionSamples + i) % sampleCount;
			float weight = sampleWeights.Evaluate((float)i/sampleCount);
			sumOfWeights += weight;
			weightedSum += positionSamples[indWrapped] * weight;
		}

		Vector3 pos = transform.position;

		lastX = pos.x;
		pos.x = weightedSum / sumOfWeights;
		float dX = pos.x - lastX;

		transform.position = pos;



		indPositionSamples = (indPositionSamples + 1) % sampleCount;
		positionSamples[indPositionSamples] = target.position.x.Clamp(-maxPan, maxPan);

		Vector3 rot = transform.localEulerAngles;
		rot.z = dX * sampleCount;
		transform.localEulerAngles = rot;
	}

	void LateUpdate()
	{

	}
}
