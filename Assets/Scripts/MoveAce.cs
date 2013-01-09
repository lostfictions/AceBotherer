using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MoveAce : MonoBehaviour
{
	public float moveSpeed = 2f;
	public float maxX = 1.7f;
	public float raycastDistance = 2.5f;

	public float SteeringForce { get; private set; }

	void Update()
	{

		Vector3 pos = transform.position;
		
		Vector3 rayOrigin = pos + Vector3.up * -0.25f;

		RaycastHit hitLeft, hitRight;
		float steeringForce = 0;

		bool didHitLeft = Physics.Raycast(rayOrigin, Vector3.forward + Vector3.right * -0.05f, out hitLeft, raycastDistance);
		bool didHitRight = Physics.Raycast(rayOrigin, Vector3.forward + Vector3.right * 0.05f, out hitRight, raycastDistance);

		// Debug.DrawRay(rayOrigin, Vector3.forward * raycastDistance, Color.blue, 0, false);
		// Debug.DrawRay(rayOrigin, Vector3.forward * raycastDistance, Color.blue, 0, false);

		if(didHitLeft || didHitRight)
		{
			float dist = Mathf.Infinity;
			int direction = 0;

			if(didHitLeft && !didHitRight)
			{
				dist = hitLeft.distance;
				direction = 1;
			}
			else if(didHitRight && !didHitLeft)
			{
				dist = hitRight.distance;
				direction = -1;
			}
			else if(hitLeft.distance < hitRight.distance)
			{
				dist = hitLeft.distance;
				direction = 1;
			}
			else
			{
				dist = hitRight.distance;
				direction = -1;
			}


			steeringForce = Mathy.Map(dist, raycastDistance, raycastDistance/2f, 0, 1f).Clamp(0, 1f);
			steeringForce *= direction;

			//Square it.
			steeringForce *= steeringForce;

			// Debug.Log("dist: " + hit.distance.ToString("G2") + " force: " + steeringForce.ToString("G2"));
		}

		float inputForce = Input.GetAxis("Horizontal");

		if(steeringForce >= 0 && inputForce > 0)
		{
			steeringForce = Mathf.Clamp(steeringForce + inputForce, 0, 1f);
		}
		else if(steeringForce <= 0 && inputForce < 0)
		{
			steeringForce = Mathf.Clamp(steeringForce + inputForce, -1f, 0);
		}

		SteeringForce = steeringForce;

		pos.x = Mathf.Clamp(pos.x + steeringForce * Time.deltaTime * moveSpeed, -maxX, maxX);
		transform.position = pos;

	}
}
