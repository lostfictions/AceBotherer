using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class AnimateAngles : MonoBehaviour
{
	public float animationSpeed = 0.2f;

	public Transform camTransform;
	public float angleThreshold = 0.2f;

	// int angleCount = 5;
	int frameWidth = 32;

	int walkFrames = 6;
	int frameHeight = 64;


	float currentDelay;
	int currentFrame;
	Vector2 textureScale;

	MoveAce moveAce;

	void Start()
	{
		moveAce = GetComponent<MoveAce>();

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

		float dX = camTransform.position.x - transform.position.x + moveAce.SteeringForce;

		int xOffset = 2;

		if(dX > 3 * angleThreshold)
			xOffset = 4;
		else if(dX > angleThreshold)
			xOffset = 3;
		else if(dX < -3 * angleThreshold)
			xOffset = 0;
		else if(dX < -angleThreshold)
			xOffset = 1;
			
		Vector2 offset = new Vector2(xOffset * textureScale.x, 1f - (currentFrame + 1) * textureScale.y);

		renderer.material.SetTextureOffset("_MainTex", offset);
	}


}
