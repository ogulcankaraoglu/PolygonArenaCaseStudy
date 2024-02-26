Shader "Vertigo/Surface/Environment"
{
    Properties
    {
        [Header(TEXTURES)]
        [Space]
        _MainTex ("Albedo", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;


        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;

        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 mainColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = mainColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
