#ifndef UNIVERSAL_INPUT_INCLUDED
#define UNIVERSAL_INPUT_INCLUDED

#define MAX_VISIBLE_LIGHTS_MOBILE  32
#define MAX_VISIBLE_LIGHTS_NON_MOBILE 256
#define USE_STRUCTURED_BUFFER_FOR_LIGHT_DATA 0

#define MAX_VISIBLE_LIGHTS_UBO  MAX_VISIBLE_LIGHTS_MOBILE
#define MAX_VISIBLE_LIGHTS_SSBO MAX_VISIBLE_LIGHTS_NON_MOBILE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderTypes.cs.hlsl"

#if defined(SHADER_API_MOBILE)
    #define MAX_VISIBLE_LIGHTS MAX_VISIBLE_LIGHTS_MOBILE
#else
    #define MAX_VISIBLE_LIGHTS MAX_VISIBLE_LIGHTS_NON_MOBILE
#endif

struct InputData
{
    float3  positionWS;
    half3   normalWS;
    half3   viewDirectionWS;
    float4  shadowCoord;
    half    fogCoord;
    half3   vertexLighting;
    half3   bakedGI;
};

///////////////////////////////////////////////////////////////////////////////
//                      Constant Buffers                                     //
///////////////////////////////////////////////////////////////////////////////

half4 _GlossyEnvironmentColor;
half4 _SubtractiveShadowColor;

float4x4 _InvCameraViewProj;
float4 _ScaledScreenParams;

float4 _MainLightPosition;
half4 _MainLightColor;

half4 _AdditionalLightsCount;
#if USE_STRUCTURED_BUFFER_FOR_LIGHT_DATA
StructuredBuffer<LightData> _AdditionalLightsBuffer;
StructuredBuffer<int> _AdditionalLightsIndices;
#else
float4 _AdditionalLightsPosition[MAX_VISIBLE_LIGHTS];
half4 _AdditionalLightsColor[MAX_VISIBLE_LIGHTS];
half4 _AdditionalLightsAttenuation[MAX_VISIBLE_LIGHTS];
half4 _AdditionalLightsSpotDir[MAX_VISIBLE_LIGHTS];
half4 _AdditionalLightsOcclusionProbes[MAX_VISIBLE_LIGHTS];
#endif

#define UNITY_MATRIX_M     unity_ObjectToWorld
#define UNITY_MATRIX_I_M   unity_WorldToObject
#define UNITY_MATRIX_V     unity_MatrixV
#define UNITY_MATRIX_I_V   unity_MatrixInvV
#define UNITY_MATRIX_P     OptimizeProjectionMatrix(glstate_matrix_projection)
#define UNITY_MATRIX_I_P   ERROR_UNITY_MATRIX_I_P_IS_NOT_DEFINED
#define UNITY_MATRIX_VP    unity_MatrixVP
#define UNITY_MATRIX_I_VP  _InvCameraViewProj
#define UNITY_MATRIX_MV    mul(UNITY_MATRIX_V, UNITY_MATRIX_M)
#define UNITY_MATRIX_T_MV  transpose(UNITY_MATRIX_MV)
#define UNITY_MATRIX_IT_MV transpose(mul(UNITY_MATRIX_I_M, UNITY_MATRIX_I_V))
#define UNITY_MATRIX_MVP   mul(UNITY_MATRIX_VP, UNITY_MATRIX_M)

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityInput.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

#endif
