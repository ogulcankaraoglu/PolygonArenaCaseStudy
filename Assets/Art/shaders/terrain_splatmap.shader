Shader "Vertigo/Terrain/Splatmap - 4 Layers"
{
    Properties
    {
        [Header(Splatmap)]
        _SplatmapTex ("SplatMap", 2D) = "white" {}
        
        [Header(BASE LAYER)]
        _BaseLayerTex ("Base Layer Albedo", 2D) = "white" {}
        _BaseLayerTiling ("Base Layer Tiling", Float) = 1
        _BaseLayerColor ("Base Layer Color", Color) = (1,1,1,1)

        [Header(SECOND LAYER)]
        _SecondLayerTex ("Second Layer Albedo", 2D) = "white" {}
        _SecondLayerTiling ("Second Layer Tiling", Float) = 1
        _SecondLayerColor ("Color", Color) = (1,1,1,1)

        [Header(THIRD LAYER)]
        _ThirdLayerTex ("Third Layer Albedo", 2D) = "white" {}
        _ThirdLayerTiling ("Third Layer Tiling", Float) = 1
        _ThirdLayerColor ("Third Layer Color", Color) = (1,1,1,1)

        [Header(FOURTH LAYER)]
        _FourthLayerTex ("Fourth Layer Albedo", 2D) = "white" {}
        _FourthLayerTiling ("Fourth Layer Tiling", Float) = 1
        _FourthLayerColor ("Fourth Layer Color", Color) = (1,1,1,1)


    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows

        #pragma target 3.0

        sampler2D _SplatmapTex;
        sampler2D _BaseLayerTex;
        sampler2D _SecondLayerTex;
        sampler2D _ThirdLayerTex;
        sampler2D _FourthLayerTex;


        struct Input
        {
            float2 uv_SplatmapTex;
        };

        half _BaseLayerTiling;
        half _SecondLayerTiling;
        half _ThirdLayerTiling;
        half _FourthLayerTiling;

        fixed4 _BaseLayerColor;
        fixed4 _SecondLayerColor;
        fixed4 _ThirdLayerColor;
        fixed4 _FourthLayerColor;

        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 splatmap = tex2D (_SplatmapTex, IN.uv_SplatmapTex);

            fixed4 finalColor = (1,1,1,1);

            fixed4 baseCol = tex2D (_BaseLayerTex, IN.uv_SplatmapTex * _BaseLayerTiling) * _BaseLayerColor;
            fixed4 secondCol = tex2D (_SecondLayerTex, IN.uv_SplatmapTex * _SecondLayerTiling) * _SecondLayerColor;
            finalColor = lerp(baseCol,secondCol,splatmap.r);

            fixed4 thirdCol = tex2D (_ThirdLayerTex, IN.uv_SplatmapTex * _ThirdLayerTiling) * _ThirdLayerColor;
            finalColor = lerp(finalColor,thirdCol,splatmap.g);

            fixed4 fourthCol = tex2D (_FourthLayerTex, IN.uv_SplatmapTex * _FourthLayerTiling) * _FourthLayerColor;
            finalColor = lerp(finalColor,fourthCol,splatmap.b);

            o.Albedo = finalColor;


            

            //o.Albedo = mainColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
