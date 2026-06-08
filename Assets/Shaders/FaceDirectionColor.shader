Shader "Custom/FaceDirectionColor"
{
    Properties
    {
        _ColorUp      ("Up Color",      Color) = (1,0,0,1)
        _ColorDown    ("Down Color",    Color) = (0,1,0,1)
        _ColorLeft    ("Left Color",    Color) = (0,0,1,1)
        _ColorRight   ("Right Color",   Color) = (1,1,0,1)
        _ColorForward ("Forward Color", Color) = (0,1,1,1)
        _ColorBack    ("Back Color",    Color) = (1,0,1,1)
        _Threshold    ("Blend Threshold", Float) = 0.7
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos      : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            float4 _ColorUp, _ColorDown, _ColorLeft, _ColorRight, _ColorForward, _ColorBack;
            float _Threshold;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // Convert normal to world space so direction is global not local
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 n = normalize(i.worldNormal);

                if      (n.y >  _Threshold) return _ColorUp;
                else if (n.y < -_Threshold) return _ColorDown;
                else if (n.x < -_Threshold) return _ColorLeft;
                else if (n.x >  _Threshold) return _ColorRight;
                else if (n.z >  _Threshold) return _ColorForward;
                else if (n.z < -_Threshold) return _ColorBack;

                // Edge faces blend between grey and face color
                return fixed4(0.5, 0.5, 0.5, 1);
            }
            ENDCG
        }
    }
}