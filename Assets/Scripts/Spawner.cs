using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
	public float scrollSpeed = 0.36f;

	public Transform[] propPrefabs;
	public float[] propSpawnWeights;

	public float propChance = 0.7f;
	public float propDelay = 0.9f;

	float propTimer;

	float sumWeights = 0;

	Queue<Transform> props = new Queue<Transform>();

	void Start()
	{
		for(int i=0; i<propSpawnWeights.Length; i++)
		{
			sumWeights += propSpawnWeights[i];

			if(i>0)
				propSpawnWeights[i] = propSpawnWeights[i] + propSpawnWeights[i-1];
		}
		

		propTimer = propDelay;

		for(int i=-6; i<35; i++)
		{
			if(Random.Range(0, 1f) < 0.4f)
			{
				SpawnProp((float)i);
			}
		}
	}
	
	void Update()
	{
		propTimer -= Time.deltaTime;
		if(propTimer <= 0)
		{
			propTimer = propDelay;

			if(Random.Range(0, 1f) < propChance)
			{
				SpawnProp(40f);
			}
		}

		int dequeueCount = 0;
		foreach(var prop in props)
		{
			Vector3 pos = prop.position;
			pos.z -= Time.deltaTime * scrollSpeed;

			if(pos.z <= -10f)
				dequeueCount++;
			else
				prop.position = pos;
		}
		for(int i=0;i<dequeueCount;i++)
			Destroy(props.Dequeue().gameObject);

	}


	void SpawnProp(float zpos)
	{
		int count = Random.Range(1, 3);

		for(int i=0; i<count; i++)
		{
			//Left or right?
			float xpos = Random.Range(0, 2) == 1 ? Random.Range(-10f, -1f) : Random.Range(1f, 10f);

			Vector3 pos = new Vector3(xpos, 0, zpos);

			Transform toSpawn = null;
			float weightVal = Random.Range(0, sumWeights);
			for(int j=0; j<propSpawnWeights.Length; j++)
			{
				if(weightVal < propSpawnWeights[j])
				{
					toSpawn = propPrefabs[j];
					break;
				}
			}

			Transform newTree = (Transform)Instantiate(toSpawn, pos, Quaternion.identity);

			newTree.parent = transform;

			props.Enqueue(newTree);
		}
	}
}
