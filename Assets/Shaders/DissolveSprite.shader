Shader "Unlit/DissolveSprite"
{
        Properties
        {
            [NoScaleOffset]_MainTex("Main Texture", 2D) = "white" {}
            _NoiseSize("NoiseSize", Float) = 5
            _StepEdge("StepEdge", Float) = 0
            [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
            [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
            [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
        }
        SubShader
        {
            Tags
            {
                "RenderPipeline"="UniversalPipeline"
                "RenderType"="Transparent"
                "UniversalMaterialType" = "Unlit"
                "Queue"="Transparent"
                "ShaderGraphShader"="true"
                "ShaderGraphTargetId"=""
            }
            Pass
            {
                Name "Sprite Unlit"
                Tags
                {
                    "LightMode" = "Universal2D"
                }
            
                // Render State
                Cull Off
                Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                ZTest LEqual
                ZWrite Off
            
                // Debug
                // <None>
            
                // --------------------------------------------------
                // Pass
            
                HLSLPROGRAM
            
                // Pragmas
                #pragma target 2.0
                #pragma exclude_renderers d3d11_9x
                #pragma vertex vert
                #pragma fragment frag
            
                // DotsInstancingOptions: <None>
                // HybridV1InjectedBuiltinProperties: <None>
            
                // Keywords
                #pragma multi_compile_fragment _ DEBUG_DISPLAY
                // GraphKeywords: <None>
            
                // Defines
                #define _SURFACE_TYPE_TRANSPARENT 1
                #define ATTRIBUTES_NEED_NORMAL
                #define ATTRIBUTES_NEED_TANGENT
                #define ATTRIBUTES_NEED_TEXCOORD0
                #define ATTRIBUTES_NEED_COLOR
                #define VARYINGS_NEED_POSITION_WS
                #define VARYINGS_NEED_TEXCOORD0
                #define VARYINGS_NEED_COLOR
                #define FEATURES_GRAPH_VERTEX
                /* WARNING: $splice Could not find named fragment 'PassInstancing' */
                #define SHADERPASS SHADERPASS_SPRITEUNLIT
                /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
            
                // Includes
                /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
            
                #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
                // --------------------------------------------------
                // Structs and Packing
            
                /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
            
                struct Attributes
                {
                     float3 positionOS : POSITION;
                     float3 normalOS : NORMAL;
                     float4 tangentOS : TANGENT;
                     float4 uv0 : TEXCOORD0;
                     float4 color : COLOR;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };
                struct Varyings
                {
                     float4 positionCS : SV_POSITION;
                     float3 positionWS;
                     float4 texCoord0;
                     float4 color;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
                struct SurfaceDescriptionInputs
                {
                     float4 uv0;
                     float4 VertexColor;
                };
                struct VertexDescriptionInputs
                {
                     float3 ObjectSpaceNormal;
                     float3 ObjectSpaceTangent;
                     float3 ObjectSpacePosition;
                };
                struct PackedVaryings
                {
                     float4 positionCS : SV_POSITION;
                     float3 interp0 : INTERP0;
                     float4 interp1 : INTERP1;
                     float4 interp2 : INTERP2;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
            
                PackedVaryings PackVaryings (Varyings input)
                {
                    PackedVaryings output;
                    ZERO_INITIALIZE(PackedVaryings, output);
                    output.positionCS = input.positionCS;
                    output.interp0.xyz =  input.positionWS;
                    output.interp1.xyzw =  input.texCoord0;
                    output.interp2.xyzw =  input.color;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
                Varyings UnpackVaryings (PackedVaryings input)
                {
                    Varyings output;
                    output.positionCS = input.positionCS;
                    output.positionWS = input.interp0.xyz;
                    output.texCoord0 = input.interp1.xyzw;
                    output.color = input.interp2.xyzw;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
            
                // --------------------------------------------------
                // Graph
            
                // Graph Properties
                CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_TexelSize;
                float _NoiseSize;
                float _StepEdge;
                CBUFFER_END
                
                // Object and Global properties
                SAMPLER(SamplerState_Linear_Repeat);
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
            
                // Graph Includes
                // GraphIncludes: <None>
            
                // -- Property used by ScenePickingPass
                #ifdef SCENEPICKINGPASS
                float4 _SelectionID;
                #endif
            
                // -- Properties used by SceneSelectionPass
                #ifdef SCENESELECTIONPASS
                int _ObjectId;
                int _PassValue;
                #endif
            
                // Graph Functions
                
                void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
                {
                    Out = A * B;
                }
                
                
                float2 Unity_GradientNoise_Dir_float(float2 p)
                {
                    // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
                    p = p % 289;
                    // need full precision, otherwise half overflows when p > 1
                    float x = float(34 * p.x + 1) * p.x % 289 + p.y;
                    x = (34 * x + 1) * x % 289;
                    x = frac(x / 41) * 2 - 1;
                    return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
                }
                
                void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
                {
                    float2 p = UV * Scale;
                    float2 ip = floor(p);
                    float2 fp = frac(p);
                    float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
                    float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
                    float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
                    float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
                    fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                    Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
                }
                
                void Unity_Clamp_float(float In, float Min, float Max, out float Out)
                {
                    Out = clamp(In, Min, Max);
                }
                
                void Unity_Step_float(float Edge, float In, out float Out)
                {
                    Out = step(Edge, In);
                }
                
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
            
                /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
            
                // Graph Vertex
                struct VertexDescription
                {
                    float3 Position;
                    float3 Normal;
                    float3 Tangent;
                };
                
                VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                {
                    VertexDescription description = (VertexDescription)0;
                    description.Position = IN.ObjectSpacePosition;
                    description.Normal = IN.ObjectSpaceNormal;
                    description.Tangent = IN.ObjectSpaceTangent;
                    return description;
                }
            
                #ifdef FEATURES_GRAPH_VERTEX
            Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
            {
            return output;
            }
            #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
            #endif
            
                // Graph Pixel
                struct SurfaceDescription
                {
                    float3 BaseColor;
                    float Alpha;
                };
                
                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    UnityTexture2D _Property_6962e328ed5f4a9f85adfa55e32fed53_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    float4 _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6962e328ed5f4a9f85adfa55e32fed53_Out_0.tex, _Property_6962e328ed5f4a9f85adfa55e32fed53_Out_0.samplerstate, _Property_6962e328ed5f4a9f85adfa55e32fed53_Out_0.GetTransformedUV(IN.uv0.xy));
                    float _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_R_4 = _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0.r;
                    float _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_G_5 = _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0.g;
                    float _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_B_6 = _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0.b;
                    float _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_A_7 = _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0.a;
                    float4 _Multiply_48dd8d7faca84fdd9e798d1a4ab94b72_Out_2;
                    Unity_Multiply_float4_float4(IN.VertexColor, _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0, _Multiply_48dd8d7faca84fdd9e798d1a4ab94b72_Out_2);
                    float _Property_61dbc58e2d9346379803a524e01100dc_Out_0 = _StepEdge;
                    float _Property_691a555122604dc1acb55b33fdba4255_Out_0 = _NoiseSize;
                    float _GradientNoise_af687b7976af4da99d70686db53191a7_Out_2;
                    Unity_GradientNoise_float(IN.uv0.xy, _Property_691a555122604dc1acb55b33fdba4255_Out_0, _GradientNoise_af687b7976af4da99d70686db53191a7_Out_2);
                    float _Clamp_5ff3c15b4b0d4f359fb0ec009d49610b_Out_3;
                    Unity_Clamp_float(_GradientNoise_af687b7976af4da99d70686db53191a7_Out_2, 0, 1, _Clamp_5ff3c15b4b0d4f359fb0ec009d49610b_Out_3);
                    float _Step_adf29b18078345f1ba239b6f0b99c21c_Out_2;
                    Unity_Step_float(_Property_61dbc58e2d9346379803a524e01100dc_Out_0, _Clamp_5ff3c15b4b0d4f359fb0ec009d49610b_Out_3, _Step_adf29b18078345f1ba239b6f0b99c21c_Out_2);
                    float _Multiply_f65379a511e74a228056c009c8c794c3_Out_2;
                    Unity_Multiply_float_float(_SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_A_7, _Step_adf29b18078345f1ba239b6f0b99c21c_Out_2, _Multiply_f65379a511e74a228056c009c8c794c3_Out_2);
                    surface.BaseColor = (_Multiply_48dd8d7faca84fdd9e798d1a4ab94b72_Out_2.xyz);
                    surface.Alpha = _Multiply_f65379a511e74a228056c009c8c794c3_Out_2;
                    return surface;
                }
            
                // --------------------------------------------------
                // Build Graph Inputs
            
                VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                {
                    VertexDescriptionInputs output;
                    ZERO_INITIALIZE(VertexDescriptionInputs, output);
                
                    output.ObjectSpaceNormal =                          input.normalOS;
                    output.ObjectSpaceTangent =                         input.tangentOS.xyz;
                    output.ObjectSpacePosition =                        input.positionOS;
                
                    return output;
                }
                
                SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
                
                    
                
                
                
                
                
                    output.uv0 =                                        input.texCoord0;
                    output.VertexColor =                                input.color;
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                
                    return output;
                }
                
            
                // --------------------------------------------------
                // Main
            
                #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteUnlitPass.hlsl"
            
                ENDHLSL
            }
            
           
            Pass
            {
                Name "Sprite Unlit"
                Tags
                {
                    "LightMode" = "UniversalForward"
                }
            
                // Render State
                Cull Off
                Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                ZTest LEqual
                ZWrite Off
            
                // Debug
                // <None>
            
                // --------------------------------------------------
                // Pass
            
                HLSLPROGRAM
            
                // Pragmas
                #pragma target 2.0
                #pragma exclude_renderers d3d11_9x
                #pragma vertex vert
                #pragma fragment frag
            
                // DotsInstancingOptions: <None>
                // HybridV1InjectedBuiltinProperties: <None>
            
                // Keywords
                #pragma multi_compile_fragment _ DEBUG_DISPLAY
                // GraphKeywords: <None>
            
                // Defines
                #define _SURFACE_TYPE_TRANSPARENT 1
                #define ATTRIBUTES_NEED_NORMAL
                #define ATTRIBUTES_NEED_TANGENT
                #define ATTRIBUTES_NEED_TEXCOORD0
                #define ATTRIBUTES_NEED_COLOR
                #define VARYINGS_NEED_POSITION_WS
                #define VARYINGS_NEED_TEXCOORD0
                #define VARYINGS_NEED_COLOR
                #define FEATURES_GRAPH_VERTEX
                /* WARNING: $splice Could not find named fragment 'PassInstancing' */
                #define SHADERPASS SHADERPASS_SPRITEFORWARD
                /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
            
                // Includes
                /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
            
                #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
                // --------------------------------------------------
                // Structs and Packing
            
                /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
            
                struct Attributes
                {
                     float3 positionOS : POSITION;
                     float3 normalOS : NORMAL;
                     float4 tangentOS : TANGENT;
                     float4 uv0 : TEXCOORD0;
                     float4 color : COLOR;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };
                struct Varyings
                {
                     float4 positionCS : SV_POSITION;
                     float3 positionWS;
                     float4 texCoord0;
                     float4 color;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
                struct SurfaceDescriptionInputs
                {
                     float4 uv0;
                     float4 VertexColor;
                };
                struct VertexDescriptionInputs
                {
                     float3 ObjectSpaceNormal;
                     float3 ObjectSpaceTangent;
                     float3 ObjectSpacePosition;
                };
                struct PackedVaryings
                {
                     float4 positionCS : SV_POSITION;
                     float3 interp0 : INTERP0;
                     float4 interp1 : INTERP1;
                     float4 interp2 : INTERP2;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
            
                PackedVaryings PackVaryings (Varyings input)
                {
                    PackedVaryings output;
                    ZERO_INITIALIZE(PackedVaryings, output);
                    output.positionCS = input.positionCS;
                    output.interp0.xyz =  input.positionWS;
                    output.interp1.xyzw =  input.texCoord0;
                    output.interp2.xyzw =  input.color;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
                Varyings UnpackVaryings (PackedVaryings input)
                {
                    Varyings output;
                    output.positionCS = input.positionCS;
                    output.positionWS = input.interp0.xyz;
                    output.texCoord0 = input.interp1.xyzw;
                    output.color = input.interp2.xyzw;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
            
                // --------------------------------------------------
                // Graph
            
                // Graph Properties
                CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_TexelSize;
                float _NoiseSize;
                float _StepEdge;
                CBUFFER_END
                
                // Object and Global properties
                SAMPLER(SamplerState_Linear_Repeat);
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
            
                // Graph Includes
                // GraphIncludes: <None>
            
                // -- Property used by ScenePickingPass
                #ifdef SCENEPICKINGPASS
                float4 _SelectionID;
                #endif
            
                // -- Properties used by SceneSelectionPass
                #ifdef SCENESELECTIONPASS
                int _ObjectId;
                int _PassValue;
                #endif
            
                // Graph Functions
                
                void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
                {
                    Out = A * B;
                }
                
                
                float2 Unity_GradientNoise_Dir_float(float2 p)
                {
                    // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
                    p = p % 289;
                    // need full precision, otherwise half overflows when p > 1
                    float x = float(34 * p.x + 1) * p.x % 289 + p.y;
                    x = (34 * x + 1) * x % 289;
                    x = frac(x / 41) * 2 - 1;
                    return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
                }
                
                void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
                {
                    float2 p = UV * Scale;
                    float2 ip = floor(p);
                    float2 fp = frac(p);
                    float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
                    float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
                    float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
                    float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
                    fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                    Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
                }
                
                void Unity_Clamp_float(float In, float Min, float Max, out float Out)
                {
                    Out = clamp(In, Min, Max);
                }
                
                void Unity_Step_float(float Edge, float In, out float Out)
                {
                    Out = step(Edge, In);
                }
                
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
            
                /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
            
                // Graph Vertex
                struct VertexDescription
                {
                    float3 Position;
                    float3 Normal;
                    float3 Tangent;
                };
                
                VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                {
                    VertexDescription description = (VertexDescription)0;
                    description.Position = IN.ObjectSpacePosition;
                    description.Normal = IN.ObjectSpaceNormal;
                    description.Tangent = IN.ObjectSpaceTangent;
                    return description;
                }
            
                #ifdef FEATURES_GRAPH_VERTEX
            Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
            {
            return output;
            }
            #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
            #endif
            
                // Graph Pixel
                struct SurfaceDescription
                {
                    float3 BaseColor;
                    float Alpha;
                };
                
                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    UnityTexture2D _Property_6962e328ed5f4a9f85adfa55e32fed53_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    float4 _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6962e328ed5f4a9f85adfa55e32fed53_Out_0.tex, _Property_6962e328ed5f4a9f85adfa55e32fed53_Out_0.samplerstate, _Property_6962e328ed5f4a9f85adfa55e32fed53_Out_0.GetTransformedUV(IN.uv0.xy));
                    float _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_R_4 = _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0.r;
                    float _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_G_5 = _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0.g;
                    float _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_B_6 = _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0.b;
                    float _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_A_7 = _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0.a;
                    float4 _Multiply_48dd8d7faca84fdd9e798d1a4ab94b72_Out_2;
                    Unity_Multiply_float4_float4(IN.VertexColor, _SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_RGBA_0, _Multiply_48dd8d7faca84fdd9e798d1a4ab94b72_Out_2);
                    float _Property_61dbc58e2d9346379803a524e01100dc_Out_0 = _StepEdge;
                    float _Property_691a555122604dc1acb55b33fdba4255_Out_0 = _NoiseSize;
                    float _GradientNoise_af687b7976af4da99d70686db53191a7_Out_2;
                    Unity_GradientNoise_float(IN.uv0.xy, _Property_691a555122604dc1acb55b33fdba4255_Out_0, _GradientNoise_af687b7976af4da99d70686db53191a7_Out_2);
                    float _Clamp_5ff3c15b4b0d4f359fb0ec009d49610b_Out_3;
                    Unity_Clamp_float(_GradientNoise_af687b7976af4da99d70686db53191a7_Out_2, 0, 1, _Clamp_5ff3c15b4b0d4f359fb0ec009d49610b_Out_3);
                    float _Step_adf29b18078345f1ba239b6f0b99c21c_Out_2;
                    Unity_Step_float(_Property_61dbc58e2d9346379803a524e01100dc_Out_0, _Clamp_5ff3c15b4b0d4f359fb0ec009d49610b_Out_3, _Step_adf29b18078345f1ba239b6f0b99c21c_Out_2);
                    float _Multiply_f65379a511e74a228056c009c8c794c3_Out_2;
                    Unity_Multiply_float_float(_SampleTexture2D_55ad4503930242e69bbc44d54bfc9ed7_A_7, _Step_adf29b18078345f1ba239b6f0b99c21c_Out_2, _Multiply_f65379a511e74a228056c009c8c794c3_Out_2);
                    surface.BaseColor = (_Multiply_48dd8d7faca84fdd9e798d1a4ab94b72_Out_2.xyz);
                    surface.Alpha = _Multiply_f65379a511e74a228056c009c8c794c3_Out_2;
                    return surface;
                }
            
                // --------------------------------------------------
                // Build Graph Inputs
            
                VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                {
                    VertexDescriptionInputs output;
                    ZERO_INITIALIZE(VertexDescriptionInputs, output);
                
                    output.ObjectSpaceNormal =                          input.normalOS;
                    output.ObjectSpaceTangent =                         input.tangentOS.xyz;
                    output.ObjectSpacePosition =                        input.positionOS;
                
                    return output;
                }
                
                SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
                
                    
                
                
                
                
                
                    output.uv0 =                                        input.texCoord0;
                    output.VertexColor =                                input.color;
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                
                    return output;
                }
                
            
                // --------------------------------------------------
                // Main
            
                #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteUnlitPass.hlsl"
            
                ENDHLSL
            }
        }
        CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
        FallBack "Hidden/Shader Graph/FallbackError"
    }