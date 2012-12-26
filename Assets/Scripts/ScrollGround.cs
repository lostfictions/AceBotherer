using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class ScrollGround : MonoBehaviour
{
	public float animationSpeed = -0.2f;
	
	void Update()
	{
		Vector2 offset = renderer.material.GetTextureOffset("_MainTex");

		offset.y += Time.deltaTime * animationSpeed;

		renderer.material.SetTextureOffset("_MainTex", offset);
	}


}
