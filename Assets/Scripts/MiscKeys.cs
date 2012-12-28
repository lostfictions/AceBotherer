using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MiscKeys : MonoBehaviour
{
	AntialiasingAsPostEffect aa;

	void Start()
	{
		aa = GetComponent<AntialiasingAsPostEffect>();
		if(!aa)
			Destroy(this);
	}
	
	void Update()
	{
		if(Input.GetKeyDown("`"))
			aa.enabled = !aa.enabled;
		else if(audio && Input.GetKeyDown("m"))
		{
			if(audio.isPlaying)
				audio.Pause();
			else
				audio.Play();
		}
	}
}
