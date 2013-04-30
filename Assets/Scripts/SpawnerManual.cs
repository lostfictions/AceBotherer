using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

using System.Linq;

public class SpawnerManual : MonoBehaviour
{
	public float scrollSpeed = 0.36f;

	public Transform propGroup;

	List<Transform> props;

	void Start()
	{
		props = new List<Transform>(propGroup.childCount + 5);

		foreach(Transform c in propGroup)
			props.Add(c);
		// props = list.OrderBy(t => t.position.z).ToArray();
	}
	
	void Update()
	{
		foreach(var prop in props)
		{
			Vector3 pos = prop.position;
			pos.z -= Time.deltaTime * scrollSpeed;
			prop.position = pos;
		}
	}

	public void AddProp(Transform toAdd)
	{
		props.Add(toAdd);
	}

}
