Shader "FX/AlphaTest"
{
	Properties
	{
		_Alpha("Alpha",Range(0,1))=0.5
		_Clip("Clip",Range(0,1))=0.2
		_K("K",Range(1,100))=10
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "LightMode"="ForwardBase"}
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha 

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#pragma multi_compile_fwdbase


			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;

				LIGHTING_COORDS(3,4)
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);

				TRANSFER_VERTEX_TO_FRAGMENT(o);
				return o;
			}

			fixed _Alpha;
			fixed _Clip;

			fixed _K;

			inline float logistic(float x)//x[0,1]
			{
				return -2/(1+exp((x-1)*_K))+2;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col=LIGHT_ATTENUATION(i);
				col.a=lerp(0,_Alpha,logistic(clamp((1-col.r-_Clip),0,1)/(1-_Clip)));
				return col;
			}
			ENDCG


		}
	}
	Fallback "Diffuse"
}
