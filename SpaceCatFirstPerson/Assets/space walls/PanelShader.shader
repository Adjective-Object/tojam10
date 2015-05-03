Shader "PanelShader" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_PanelColor ("Panel Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_PanelTex ("Panel (RGB)", 2D) = "white" {}
}
SubShader {
	Tags { "RenderType"="Transparent" }
	LOD 200

	Pass {
            // Apply base texture
            SetTexture [_MainTex] {
                combine texture
            }
            // Blend in the alpha texture using the lerp operator
            SetTexture [_PanelTex] {
                combine texture lerp (texture) previous
            }
	}

	CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _MainTex;
	sampler2D _PanelTex;
	fixed4 _Color;
	fixed4 _PanelColor;
	float _Blend1 = 1.0;

	struct Input {
		float2 uv_MainTex;
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 mainCol = tex2D(_MainTex, IN.uv_MainTex) * _Color;
        fixed4 texTwoCol = tex2D(_PanelTex, IN.uv_MainTex);                           
      
        fixed4 mainOutput = mainCol.rgba * (1.0 - (texTwoCol.a * 0.0));
        fixed4 blendOutput = texTwoCol.rgba * texTwoCol.a * 1.0 * _PanelColor;         
      
        o.Albedo = mainOutput.rgb + blendOutput.rgb;
        o.Alpha = mainOutput.a + blendOutput.a;
	}
	ENDCG
}


Fallback "Legacy Shaders/Diffuse"
}
