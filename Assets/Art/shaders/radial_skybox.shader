Shader "Vertigo/Skybox/Radial Gradient Skybox" {
	Properties {
		_Color1 ("Inside Color", Color) = (1.0, 1.0, 1.0, 0)
		_Color2 ("Outside Color", Color) = (0.25, 0.25, 0.25, 0)
		_GradientIntensity("Gradient Intensity", Float) = 0.5
	}
	SubShader {
		Tags { "RenderType" = "Background" "Queue" = "Background" "PreviewType" = "Skybox" }
		Pass {
			ZWrite Off
			Cull Off

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			fixed3 _Color1;
			fixed3 _Color2;
			fixed _GradientIntensity;

			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_TARGET {
				fixed2 uv = (i.vertex.xy / _ScreenParams.x - _ScreenParams.xy / _ScreenParams.x * 0.5) * _GradientIntensity;
				fixed mask = length(uv);
				fixed4 finalCol = fixed4(lerp(_Color1, _Color2, mask), 1);
				return finalCol;
			}

			ENDCG
		}
	}
}