Shader "Decal/NormalDecal"
{
    Properties
    {
        _AlphaTex("Alpha", 2D) = "white" {}
        _NormalTex ("Normal", 2D) = "bump" {}
    }
    SubShader
    {
        Name "DEFERRED"
        Tags {
            "LightMode" = "Deferred"
            "Queue" = "Geometry+10"
            }
        LOD 100
 
        Pass
        {
        Offset -1, -1
        zwrite off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
           
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
 
                  float4 tSpace0 : TEXCOORD1;
                  float4 tSpace1 : TEXCOORD2;
                  float4 tSpace2 : TEXCOORD3;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _NormalTex;
            float4 _NormalTex_ST;
            sampler2D _AlphaTex;
           
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _NormalTex);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
                fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
                o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
                o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
                o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z); 
                return o;
            }

            void frag (v2f i, out half4 outGBuffer2 : SV_Target2)
            {
 
                half3 norm = UnpackNormal(tex2D(_NormalTex, i.uv));
                clip(tex2D(_AlphaTex,i.uv).a - 0.5);
                fixed3 worldN;
                worldN.x = dot(i.tSpace0.xyz, norm);
                worldN.y = dot(i.tSpace1.xyz, norm);
                worldN.z = dot(i.tSpace2.xyz, norm);
                worldN = worldN * 0.5f + 0.5f;
                outGBuffer2 = float4( worldN, 0);
            }
            ENDCG
        }
    }
}