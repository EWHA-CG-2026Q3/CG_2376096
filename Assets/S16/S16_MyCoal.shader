// 실습 2: 숯 재질. 재질에서 Base Color와 Emission Color를 받아 더해서 출력
Shader "CG/S16_MyCoal"
{
    // Properties: 재질 Inspector에 보이는 칸 (SubShader 바깥, 위쪽)
    Properties
    {
        _BaseColor ("Base Color", Color) = (0, 0, 0, 1)
        [HDR] _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 변수 이름은 Properties의 이름과 같아야 재질 값이 들어옴
            float4 _BaseColor;
            float4 _EmissionColor;

            float4 vert(float3 positionOS : POSITION) : SV_POSITION
            {
                return TransformObjectToHClip(positionOS);
            }

            float4 frag() : SV_Target
            {
                // 빛 계산 없이 두 색을 더함. Emission이 1을 넘으면 Bloom으로 번짐
                return float4(_BaseColor.rgb + _EmissionColor.rgb, 1);
            }
            ENDHLSL
        }
    }
}
