using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CamFollowAndRoll : MonoBehaviour
{
	public Transform target;

	public float panTime = 1f;
	public float maxPanSpeed = 0.01f;

	public float maxPan = 1f;
	
	// public float maxRoll = 6f;

	float curPanVel = 0;

	void Update()
	{
		Vector3 pos = transform.position;
		pos.x = Mathf.SmoothDamp(pos.x, Mathf.Clamp(target.position.x, -maxPan, maxPan), ref curPanVel, panTime, maxPanSpeed);
		transform.position = pos;

		// float targetRoll = Mathf.Clamp(curPanVel * 6, -maxRoll, maxRoll);

		Vector3 rot = transform.localEulerAngles;
		rot.z = curPanVel * 10;
		// rot.z = targetRoll;
		// rot.z = Mathf.SmoothDampAngle(rot.z, targetRoll, ref curRollVel, rollSpeed);
		transform.localEulerAngles = rot;
	}
}
