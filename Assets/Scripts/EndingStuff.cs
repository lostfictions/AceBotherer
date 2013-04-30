using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EndingStuff : MonoBehaviour
{
	public SpawnerManual scroller;
	public MoveAce movement;

	public Transform ace;
	public MeshRenderer endingAce;


	public Transform cam;

	public MeshRenderer camFade;
	public MeshRenderer endTextFade1;
	public MeshRenderer endTextFade2;


	public Transform endSkull;

	CamFollowAndRoll camfollow;

	bool sequenceStarted = false;


	void Start()
	{
		camfollow = GetComponent<CamFollowAndRoll>();
		camfollow.applyNoise = false;

		audio.volume = 0;

		movement.enabled = false;

		camFade.enabled = true;

		Vector3 targetPos = cam.position;
		Vector3 startPos = cam.position + Vector3.forward * -3f;

		Waiters.Wait(2f)
			.ThenInterpolate(5f, f => audio.volume = Easing.EaseInOut(f, EaseType.Sine) );

		Waiters.Interpolate(4f, f =>
			{
				float e = Easing.EaseIn(f, EaseType.Quad);
				camFade.material.color = new Color(0, 0, 0, 1f - e);
			})
			.Then( () => camFade.enabled = false);

		Waiters.Interpolate(6f, f => cam.position = Vector3.Lerp(startPos, targetPos, Easing.EaseInOut(f, EaseType.Quad)))
			.Then( () => { movement.enabled = true; camfollow.applyNoise = true; } );
	}

	void Update()
	{
		if(!sequenceStarted && endSkull.position.z < -7.93f)
		{
			sequenceStarted = true;

			scroller.AddProp(ace);

			ace.GetComponent<MeshRenderer>().enabled = false;
			endingAce.enabled = true;

			Waiters.Interpolate(1.5f, f => {
					Vector3 pos = cam.position;
					pos.z -= (scroller.scrollSpeed * f) * Time.deltaTime;
					cam.position = pos; })
				.ThenInterpolate(15f, f => {
					Vector3 pos = cam.position;
					pos.z -= (scroller.scrollSpeed + Easing.EaseIn(f * 2, EaseType.Quintic)) * Time.deltaTime;
					cam.position = pos; });

			camFade.enabled = true;
			endTextFade1.enabled = true;
			endTextFade2.enabled = true;

			endTextFade1.material.color = Color.clear;
			endTextFade2.material.SetColor("_TintColor", Color.clear);

			Waiters.Wait(3f)
				.Then( ()=> endingAce.material.SetTextureOffset("_MainTex", new Vector2(0.25f, 0)) )
				.ThenWait(1.3f)
				.Then( ()=> endingAce.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0)) )
				.ThenWait(7f)
				.ThenInterpolate(2f, f => camFade.material.color = new Color(0, 0, 0, f))
				.Then( () => camfollow.applyNoise = false )
				.ThenWait(1.2f)
				.ThenInterpolate(1f, f => {
					endTextFade1.material.color = Color.Lerp(Color.clear, Color.white, Easing.EaseIn(f, EaseType.Quintic));
					endTextFade2.material.SetColor("_TintColor", Color.Lerp(Color.clear, Color.white, Easing.EaseInOut(f, EaseType.Back)));
					// endTextFade2.material.color = Color.Lerp(Color.clear, Color.white, Easing.EaseIn(f, EaseType.Back));
				})
				.ThenInterpolate(0.5f, f => endTextFade2.material.SetColor("_TintColor", Color.Lerp(Color.white, Color.clear, Easing.EaseOut(f, EaseType.Quad))) );

		}
	}
}
