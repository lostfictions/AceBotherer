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
	public float rollFactor = 1f;

	//Perlin position-noise params
	public float noiseVertDxMultiplier = 10f;
	public float noiseVertConstMultiplier = 0f;
	public float noiseVertTimeScale = 1f;
	public float noiseHorizDxMultiplier = 1f;
	public float noiseHorizConstMultiplier = 0.1f;
	public float noiseHorizTimescale = 1f;

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
			//our array of samples is wrapped, so modulo to find our adjusted index
			int indWrapped = (indPositionSamples + i) % sampleCount;
			//evaluate how much weight our current sample should have
			float weight = sampleWeights.Evaluate((float)i/sampleCount);
			//we need the sum of all weight values to use as a divisor for our weighted average
			sumOfWeights += weight;
			weightedSum += positionSamples[indWrapped] * weight;
		}

		Vector3 pos = transform.position;

		lastX = pos.x;
		pos.x = weightedSum / sumOfWeights;
		float dX = pos.x - lastX;


		float noiseVert = (dX * noiseVertDxMultiplier + noiseVertConstMultiplier) * (Mathf.PerlinNoise(0f, Time.time * noiseVertTimeScale) - 0.5f);
		pos.y = 1f + noiseVert;

		transform.position = pos;

		float noiseHoriz = (dX * noiseHorizDxMultiplier + noiseHorizConstMultiplier) * (Mathf.PerlinNoise(Time.time * noiseHorizTimescale, 0.0f) - 0.5f);

		indPositionSamples = (indPositionSamples + 1) % sampleCount;
		positionSamples[indPositionSamples] = target.position.x.Clamp(-maxPan, maxPan) + noiseHoriz;

		//apply roll
		Vector3 rot = transform.localEulerAngles;
		rot.z = dX * sumOfWeights * rollFactor;
		rot.y = dX * sumOfWeights * rollFactor;
		transform.localEulerAngles = rot;
	}

}
