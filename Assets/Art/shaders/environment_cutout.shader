Shader "Vertigo/Transparent/Environment Cutout"
{
    Properties
    {
        [Header(TEXTURES)]
        [Space]
        _MainTex ("Albedo", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Cutout ("Cutout Value", Float) = 0.5

    }
    SubShader
    {
        Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
        LOD 200
        Cull Off

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;


        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        half _Cutout;

        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 mainColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = mainColor.rgb;
            clip(mainColor.a - _Cutout);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
