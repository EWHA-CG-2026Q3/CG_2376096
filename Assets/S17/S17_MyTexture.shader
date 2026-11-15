// S17: 텍스처 셰이더. uv로 그림(_BaseMap)의 어느 위치를 읽을지 정함
Shader "CG/S17_MyTexture"
{
    Properties
    {
        _BaseMap ("Base Map", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            sampler2D _BaseMap;

            // vert: 위치는 Clip Space로 바꾸고, uv는 그대로 frag에 넘김
            void vert(float3 positionOS : POSITION,
                      float2 uv : TEXCOORD0,
                      out float2 uvOut : TEXCOORD0,
                      out float4 positionCS : SV_POSITION)
            {
                uvOut = uv;
                positionCS = TransformObjectToHClip(positionOS);
            }

            float4 frag(float2 uv : TEXCOORD0) : SV_Target
            {
                // 과제: D3 칸(u 0.50~0.75, v 0.00~0.25)만 표면 전체에 보이게 함
                // - 곱하는 값 0.25: 표면 uv 0~1 전체를 그림의 한 칸(폭 0.25)에 압축
                // - 더하는 값 (0.5, 0): D3 칸의 왼쪽 아래 모서리 위치
                float2 p = uv * 0.25 + float2(0.5, 0);

                // 미션 1: B2 한 칸만 표면 전체에
                // p = uv * 0.25 + float2(0.25, 0.5);

                // 미션 2: 그림 전체를 표면의 왼쪽 아래 1/4에
                // p = uv * 2;
                // if (p.x > 1 || p.y > 1) return float4(0.5, 0.5, 0.5, 1);

                return tex2D(_BaseMap, p);
            }
            ENDHLSL
        }
    }
}
