Shader "Unlit/Cartoon"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_Offset ("Hue", Range(0,3)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Cull Front
		//zwrite off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				fixed3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			fixed4 _Color;
			float _Offset;

			v2f vert (appdata v)
			{
				v2f o;
				float4 weight = float4(v.normal.x, v.normal.y, v.normal.z, 0.0);
				o.vertex = UnityObjectToClipPos(v.vertex + weight * _Offset);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _Color;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
