// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/VertexAlphaHeightBlend"
{
	Properties
	{
		_TessValue( "Max Tessellation", Range( 1, 32 ) ) = 16
		_TessMin( "Tess Min Distance", Float ) = 2
		_TessMax( "Tess Max Distance", Float ) = 4
		_Cutoff( "Mask Clip Value", Float ) = 0.98
		_TexturesCom_MuddySand2_2x2_1K_albedo("TexturesCom_MuddySand2_2x2_1K_albedo", 2D) = "white" {}
		_Float2("Float 2", Range( 0 , 1)) = 0.20462
		_TexturesCom_MuddySand2_2x2_1K_height("TexturesCom_MuddySand2_2x2_1K_height", 2D) = "white" {}
		_TexturesCom_MuddySand2_2x2_1K_normal("TexturesCom_MuddySand2_2x2_1K_normal", 2D) = "bump" {}
		_TexturesCom_MuddySand2_2x2_1K_roughness("TexturesCom_MuddySand2_2x2_1K_roughness", 2D) = "white" {}
		_TexturesCom_MuddySand2_2x2_1K_ao("TexturesCom_MuddySand2_2x2_1K_ao", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Back
		CGPROGRAM
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _TexturesCom_MuddySand2_2x2_1K_height;
		uniform float4 _TexturesCom_MuddySand2_2x2_1K_height_ST;
		uniform float _Float2;
		uniform sampler2D _TexturesCom_MuddySand2_2x2_1K_normal;
		uniform float4 _TexturesCom_MuddySand2_2x2_1K_normal_ST;
		uniform sampler2D _TexturesCom_MuddySand2_2x2_1K_albedo;
		uniform float4 _TexturesCom_MuddySand2_2x2_1K_albedo_ST;
		uniform sampler2D _TexturesCom_MuddySand2_2x2_1K_roughness;
		uniform float4 _TexturesCom_MuddySand2_2x2_1K_roughness_ST;
		uniform sampler2D _TexturesCom_MuddySand2_2x2_1K_ao;
		uniform float4 _TexturesCom_MuddySand2_2x2_1K_ao_ST;
		uniform float _Cutoff = 0.98;
		uniform float _TessValue;
		uniform float _TessMin;
		uniform float _TessMax;


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityDistanceBasedTess( v0.vertex, v1.vertex, v2.vertex, _TessMin, _TessMax, _TessValue );
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 uv_TexturesCom_MuddySand2_2x2_1K_height = v.texcoord * _TexturesCom_MuddySand2_2x2_1K_height_ST.xy + _TexturesCom_MuddySand2_2x2_1K_height_ST.zw;
			float4 tex2DNode2 = tex2Dlod( _TexturesCom_MuddySand2_2x2_1K_height, float4( uv_TexturesCom_MuddySand2_2x2_1K_height, 0, 0.0) );
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( ( tex2DNode2.r * _Float2 ) * ase_vertexNormal );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexturesCom_MuddySand2_2x2_1K_normal = i.uv_texcoord * _TexturesCom_MuddySand2_2x2_1K_normal_ST.xy + _TexturesCom_MuddySand2_2x2_1K_normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _TexturesCom_MuddySand2_2x2_1K_normal, uv_TexturesCom_MuddySand2_2x2_1K_normal ) );
			float2 uv_TexturesCom_MuddySand2_2x2_1K_albedo = i.uv_texcoord * _TexturesCom_MuddySand2_2x2_1K_albedo_ST.xy + _TexturesCom_MuddySand2_2x2_1K_albedo_ST.zw;
			o.Albedo = tex2D( _TexturesCom_MuddySand2_2x2_1K_albedo, uv_TexturesCom_MuddySand2_2x2_1K_albedo ).rgb;
			o.Metallic = 0.0;
			float2 uv_TexturesCom_MuddySand2_2x2_1K_roughness = i.uv_texcoord * _TexturesCom_MuddySand2_2x2_1K_roughness_ST.xy + _TexturesCom_MuddySand2_2x2_1K_roughness_ST.zw;
			o.Smoothness = tex2D( _TexturesCom_MuddySand2_2x2_1K_roughness, uv_TexturesCom_MuddySand2_2x2_1K_roughness ).r;
			float2 uv_TexturesCom_MuddySand2_2x2_1K_ao = i.uv_texcoord * _TexturesCom_MuddySand2_2x2_1K_ao_ST.xy + _TexturesCom_MuddySand2_2x2_1K_ao_ST.zw;
			o.Occlusion = tex2D( _TexturesCom_MuddySand2_2x2_1K_ao, uv_TexturesCom_MuddySand2_2x2_1K_ao ).r;
			o.Alpha = 1;
			float2 uv_TexturesCom_MuddySand2_2x2_1K_height = i.uv_texcoord * _TexturesCom_MuddySand2_2x2_1K_height_ST.xy + _TexturesCom_MuddySand2_2x2_1K_height_ST.zw;
			float4 tex2DNode2 = tex2D( _TexturesCom_MuddySand2_2x2_1K_height, uv_TexturesCom_MuddySand2_2x2_1K_height );
			float4 temp_cast_3 = (tex2DNode2.r).xxxx;
			clip( ( (CalculateContrast(1.0,temp_cast_3)).r + i.vertexColor.r ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
0;92;867;779;2310.95;-64.83797;2.789929;True;False
Node;AmplifyShaderEditor.RangedFloatNode;11;-1117.183,214.1814;Float;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1002.868,848.4279;Float;True;Property;_TexturesCom_MuddySand2_2x2_1K_height;TexturesCom_MuddySand2_2x2_1K_height;8;0;Create;True;0;0;False;0;d9ff59a43fd4acb459f9d2c11077afe5;a2d19433adbc2bd47b44c30d0f359a62;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleContrastOpNode;10;-928.4304,353.1614;Float;False;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1065.886,1299.317;Float;False;Property;_Float2;Float 2;7;0;Create;True;0;0;False;0;0.20462;0.036;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;12;-636.9172,349.5863;Float;False;FLOAT;0;0;0;0;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;6;-1233.542,553.3333;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;16;-756.2145,1440.328;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-643.3426,1115.74;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-719.5487,610.0734;Float;True;Property;_TexturesCom_MuddySand2_2x2_1K_normal;TexturesCom_MuddySand2_2x2_1K_normal;9;0;Create;True;0;0;False;0;500142944ea943b46a56f00b6a340aef;c80c12384f69091479abc5f8b456c1af;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-721.8318,120.5074;Float;True;Property;_TexturesCom_MuddySand2_2x2_1K_ao;TexturesCom_MuddySand2_2x2_1K_ao;11;0;Create;True;0;0;False;0;2c4a9ffb33523454f9dab64733b12f6c;cf28035de432382449901960e12b0a18;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-306.5562,66.27261;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-612.2725,-131.0214;Float;True;Property;_TexturesCom_MuddySand2_2x2_1K_albedo;TexturesCom_MuddySand2_2x2_1K_albedo;6;0;Create;True;0;0;False;0;2e4cce2c61b867042b061a42e2c78f1b;dfaadd0bb9bd66a4f8b4ce4005021dbb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-427.3411,1250.145;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;4;-1081.27,-27.39272;Float;True;Property;_TexturesCom_MuddySand2_2x2_1K_roughness;TexturesCom_MuddySand2_2x2_1K_roughness;10;0;Create;True;0;0;False;0;a0d945608a8122c4ab76d1ed124fb51f;8d38d7335b7ce6840a8ce109e1231ecb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-293.1919,405.6332;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;Custom/VertexAlphaHeightBlend;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.98;True;True;0;False;TransparentCutout;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;0;16;2;4;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;5;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;1;2;1
WireConnection;10;0;11;0
WireConnection;12;0;10;0
WireConnection;14;0;2;1
WireConnection;14;1;17;0
WireConnection;15;0;14;0
WireConnection;15;1;16;0
WireConnection;9;0;12;0
WireConnection;9;1;6;1
WireConnection;0;0;1;0
WireConnection;0;1;3;0
WireConnection;0;3;5;0
WireConnection;0;4;4;0
WireConnection;0;5;13;0
WireConnection;0;10;9;0
WireConnection;0;11;15;0
ASEEND*/
//CHKSM=D67B918C5253397C312A12C8B9B297BE70CDAEAF