using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class AnimateAngles : MonoBehaviour
{
	public float animationSpeed = 0.2f;

	int angleCount = 5;
	int frameWidth = 32;

	int walkFrames = 6;
	int frameHeight = 64;


	float currentDelay;
	int currentFrame;
	Vector2 textureScale;

	void Start()
	{
		currentDelay = animationSpeed;


		int columnCount = renderer.material.mainTexture.width / frameWidth;
		int rowCount = renderer.material.mainTexture.height / frameHeight;

		textureScale = new Vector2(1.0f / columnCount, 1.0f / rowCount);

		renderer.material.SetTextureScale("_MainTex", textureScale);
	}
	
	void Update()
	{
		currentDelay -= Time.deltaTime;
		
		if (currentDelay < 0)
		{
			currentFrame = (currentFrame + 1) % walkFrames;
			currentDelay = animationSpeed;
		}
		
		Vector2 offset = new Vector2(2 * textureScale.x, 1f - (currentFrame + 1) * textureScale.y);

		renderer.material.SetTextureOffset("_MainTex", offset);
	}


}
