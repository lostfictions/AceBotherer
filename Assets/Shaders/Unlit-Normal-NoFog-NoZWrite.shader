// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/Unlit Texture No Fog No ZWrite" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader {
	Tags {"RenderType"="Opaque" }
	LOD 100
	
	Pass {
		Fog { Mode Off }
		Lighting Off
		ZWrite Off
		SetTexture [_MainTex] { combine texture } 
	}
}
}
