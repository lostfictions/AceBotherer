using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Animate : MonoBehaviour
{
	public float animationSpeed = 0.2f;
	
	public int walkFrames = 2;
	// const int additionalColumnFrames = 1;

	// public int frameWidth = 32;
	// public int frameHeight = 64;

	float currentDelay;
	int currentFrame;
	Vector2 textureScale;

	void Start ()
	{
		currentDelay = animationSpeed;


		// int columnCount = renderer.material.mainTexture.width / frameWidth;
		// int rowCount = renderer.material.mainTexture.height / frameHeight;

		textureScale = new Vector2(1.0f / walkFrames, 1.0f);
		// textureScale = new Vector2(1.0f / columnCount, 1.0f / rowCount);

		renderer.material.SetTextureScale("_MainTex", textureScale);
	}
	
	void Update ()
	{
		currentDelay -= Time.deltaTime;
		
		if (currentDelay < 0)
		{
			currentFrame = (currentFrame + 1) % walkFrames;
			currentDelay = animationSpeed;
		}
		
		Vector2 offset = new Vector2(currentFrame * textureScale.x, 0);

		renderer.material.SetTextureOffset("_MainTex", offset);
	}


}
