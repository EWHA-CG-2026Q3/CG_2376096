// 실습 1: 최소한의 셰이더 (모든 픽셀을 한 가지 색으로 칠함)
Shader "CG/S16_MyShader"
{
    SubShader
    {
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // vert: 정점마다 한 번. Object Space 위치를 Clip Space로 변환(P x V x M)
            float4 vert(float3 positionOS : POSITION) : SV_POSITION
            {
                return TransformObjectToHClip(positionOS);
            }

            // frag: 픽셀마다 한 번. 빛 계산이 없어서 입체감 없는 한 가지 색(빨강)
            float4 frag() : SV_Target
            {
                return float4(1, 0, 0, 1);
            }
            ENDHLSL
        }
    }
}
