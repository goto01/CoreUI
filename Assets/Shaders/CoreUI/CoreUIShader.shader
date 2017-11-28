Shader "CoreUI/CoreUISimpleShader" 
{
	Properties 
	{
		_YTopLimit ("Top limit", range(-10, 10)) = 0
		_YBottomLimit ("Bot limit", range(-10, 10)) = 0
		_XLeftLimit ("Left limit", range(-10, 10)) = 0
		_XRightLimit ("Right limit", range(-10, 10)) = 0
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	
	SubShader 
	{
		Tags 
		{ 
			"Queue"="Transparent"
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
		}
		LOD 200
		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
	
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};
	
			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 originVertex : TEXCOORD1;
			};
			
			fixed4 _Color;
			uniform fixed _YTopLimit;
			uniform fixed _YBottomLimit;
			uniform fixed _XLeftLimit;
			uniform fixed _XRightLimit;
	
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				OUT.originVertex = mul(unity_ObjectToWorld, IN.vertex);
				return OUT;
			}
	
			sampler2D _MainTex;
	
			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
				int top = max(0, sign(_YTopLimit - IN.originVertex.y));
				int bot = max(0, sign(IN.originVertex.y - _YBottomLimit));
				int left = max(0, sign(IN.originVertex.x - _XLeftLimit));
				int right = max(0, sign(_XRightLimit - IN.originVertex.x));
				c.a = min(c.a, top * bot * left * right);
				c.rgb *= c.a;
				return c;
			}
			
			ENDCG
		}
	}
}
