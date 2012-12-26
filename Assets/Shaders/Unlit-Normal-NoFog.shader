// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/Unlit Texture No Fog" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass {
		Fog { Mode Off }
		Lighting Off
		SetTexture [_MainTex] { combine texture } 
	}
}
}
