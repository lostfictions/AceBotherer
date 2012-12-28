using UnityEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Camera))]
public class UpscaleCamera : MonoBehaviour
{
	public int ratio = 2;

	Material rtMat;

	void Start()
	{
		string unlitShader = 
			@"
				Shader ""Custom/Unlit Texture No Fog"" {
					Properties {
						_MainTex (""Base (RGB)"", 2D) = ""white"" {}
					}

					SubShader {
						Tags { ""RenderType""=""Opaque"" }
						LOD 100
						
						Pass {
							Fog { Mode Off }
							Lighting Off
							SetTexture [_MainTex] { combine texture } 
						}
					}
				}
			";


		RenderTexture rt = new RenderTexture(Screen.width / ratio, Screen.height / ratio, 16);
		rt.filterMode = FilterMode.Point;
		
		camera.targetTexture = rt;

		GameObject go = new GameObject("RenderCam");
		go.transform.position = new Vector3(0, -100f, 0);
		go.transform.localEulerAngles = new Vector3(90f, 0, 0);


		var ren = go.AddComponent<MeshRenderer>();
		rtMat = new Material(unlitShader);
		rtMat.mainTexture = rt;
		ren.sharedMaterial = rtMat;
		
		Camera cam = go.AddComponent<Camera>();
		cam.orthographic = true;
		cam.orthographicSize = Screen.height/2;

		float right = Screen.width/2;
		float top = Screen.height/2;
		float depth = 0.31f;

		Mesh mesh = new Mesh();
		var verts = new Vector3[4];
		verts[0] = new Vector3(-right, -top, depth);
		verts[1] = new Vector3(right, -top, depth);
		verts[2] = new Vector3(right, top, depth);
		verts[3] = new Vector3(-right, top, depth);

		var uvs = new Vector2[4];
		uvs[0] = new Vector2(0,  0);
		uvs[1] = new Vector2(1f, 0);
		uvs[2] = new Vector2(1f, 1f);
		uvs[3] = new Vector2(0,  1f);

		var tris = new int[6];
		tris[0] = 0;
		tris[1] = 3;
		tris[2] = 2;
		tris[3] = 0;
		tris[4] = 2;
		tris[5] = 1;

		mesh.vertices = verts;
		mesh.uv = uvs;
		mesh.triangles = tris;

		go.AddComponent<MeshFilter>().mesh = mesh;
	}

	void Update()
	{
		bool changed = false;
		if(Input.GetKeyDown("1"))
		{
			ratio = 1;
			changed = true;
		}
		else if(Input.GetKeyDown("2"))
		{
			ratio = 2;
			changed = true;	
		}
		else if(Input.GetKeyDown("3"))
		{
			ratio = 4;
			changed = true;
		}
		else if(Input.GetKeyDown("4"))
		{
			ratio = 8;
			changed = true;
		}
		else if(Input.GetKeyDown("5"))
		{
			ratio = 16;
			changed = true;
		}

		if(changed)
		{
			Destroy(camera.targetTexture);
			var rt = new RenderTexture(Screen.width / ratio, Screen.height / ratio, 16);
			rt.filterMode = FilterMode.Point;
			camera.targetTexture = rt;
			rtMat.mainTexture = rt;
		}

	}
}
