Shader "Vertigo/Surface/Weapon Standard"
{
    Properties
    {
        [Header(TEXTURES)]
        [Space]
        _MainTex ("Albedo", 2D) = "white" {}
        _MOER ("MOER(Metallic - Occlusion - Emissive - Smoothness)", 2D) = "black" {}
        _AORGB ("AO-RGB(AO - Position - World Normals)", 2D) = "white" {}

        [Header(SETTINGS)]
        [Space]
        _Color ("Main Color", Color) = (1,1,1,1)
        [HDR] _EmissionColor ("Emission Color", Color) = (0,0,0,0)
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _MOER;
        sampler2D _AORGB;
        sampler2D _GridTex;


        struct Input
        {
            float2 uv_MainTex;
            float2 uv2_GridTex;
            float2 uv2_AORGB;
        };

        half _Smoothness;
        half _Metallic;
        half _GridTiling;
        half _GridOpacity;
        fixed4 _Color;
        fixed4 _EmissionColor;
        fixed4 _VertexOffset;


        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 mainColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 moer = tex2D (_MOER, IN.uv_MainTex);
            fixed4 aorgb = tex2D (_AORGB, IN.uv2_AORGB);

            o.Albedo = mainColor.rgb;
            o.Metallic = moer.r * _Metallic;
            o.Emission = moer.b * _EmissionColor;
            o.Smoothness = moer.a * _Smoothness;
            o.Occlusion = aorgb.r;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
