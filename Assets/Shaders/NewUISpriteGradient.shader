Shader "NewUISpriteGradient"
    {
        Properties
        {
            [NoScaleOffset]_MainTex("Main Texture", 2D) = "white" {}
            _Color("Color", Color) = (1, 0.4858491, 0.4858491, 0)
            _Gradient_Height("Gradient Height", Float) = 0.85
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
                float4 _Color;
                float _Gradient_Height;
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
                
                void Unity_Clamp_float(float In, float Min, float Max, out float Out)
                {
                    Out = clamp(In, Min, Max);
                }
                
                void Unity_Power_float(float A, float B, out float Out)
                {
                    Out = pow(A, B);
                }
                
                void Unity_OneMinus_float(float In, out float Out)
                {
                    Out = 1 - In;
                }
                
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
                
                void Unity_Saturate_float(float In, out float Out)
                {
                    Out = saturate(In);
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
                    UnityTexture2D _Property_e3f1be0cae404672b5b906572aa44bbd_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    float4 _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_e3f1be0cae404672b5b906572aa44bbd_Out_0.tex, _Property_e3f1be0cae404672b5b906572aa44bbd_Out_0.samplerstate, _Property_e3f1be0cae404672b5b906572aa44bbd_Out_0.GetTransformedUV(IN.uv0.xy));
                    float _SampleTexture2D_58daf8465f844985a17d31905c909f3e_R_4 = _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0.r;
                    float _SampleTexture2D_58daf8465f844985a17d31905c909f3e_G_5 = _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0.g;
                    float _SampleTexture2D_58daf8465f844985a17d31905c909f3e_B_6 = _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0.b;
                    float _SampleTexture2D_58daf8465f844985a17d31905c909f3e_A_7 = _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0.a;
                    float4 _Multiply_54c1b0eebb3641f899ac4a082ce86bf1_Out_2;
                    Unity_Multiply_float4_float4(IN.VertexColor, _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0, _Multiply_54c1b0eebb3641f899ac4a082ce86bf1_Out_2);
                    float4 _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0 = IN.uv0;
                    float _Split_0ad2ed23bf6a497183a7d660e97962bc_R_1 = _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0[0];
                    float _Split_0ad2ed23bf6a497183a7d660e97962bc_G_2 = _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0[1];
                    float _Split_0ad2ed23bf6a497183a7d660e97962bc_B_3 = _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0[2];
                    float _Split_0ad2ed23bf6a497183a7d660e97962bc_A_4 = _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0[3];
                    float _Clamp_93a7abe938c34e4eb5db7f90bc5931a9_Out_3;
                    Unity_Clamp_float(_Split_0ad2ed23bf6a497183a7d660e97962bc_G_2, 0, 1, _Clamp_93a7abe938c34e4eb5db7f90bc5931a9_Out_3);
                    float _Property_3b3188b7a8d141a380525d46398c8f72_Out_0 = _Gradient_Height;
                    float _Power_629dbb45078f414baba7509e0113a79c_Out_2;
                    Unity_Power_float(_Clamp_93a7abe938c34e4eb5db7f90bc5931a9_Out_3, _Property_3b3188b7a8d141a380525d46398c8f72_Out_0, _Power_629dbb45078f414baba7509e0113a79c_Out_2);
                    float _OneMinus_4312b2145c684a10bc8a07ef92676a19_Out_1;
                    Unity_OneMinus_float(_Power_629dbb45078f414baba7509e0113a79c_Out_2, _OneMinus_4312b2145c684a10bc8a07ef92676a19_Out_1);
                    float _Multiply_970e04d2cf4c459e84bf9f5031cdfd80_Out_2;
                    Unity_Multiply_float_float(_SampleTexture2D_58daf8465f844985a17d31905c909f3e_A_7, _OneMinus_4312b2145c684a10bc8a07ef92676a19_Out_1, _Multiply_970e04d2cf4c459e84bf9f5031cdfd80_Out_2);
                    float _Saturate_0a167b172160490698449e5e903dd020_Out_1;
                    Unity_Saturate_float(_Multiply_970e04d2cf4c459e84bf9f5031cdfd80_Out_2, _Saturate_0a167b172160490698449e5e903dd020_Out_1);
                    surface.BaseColor = (_Multiply_54c1b0eebb3641f899ac4a082ce86bf1_Out_2.xyz);
                    surface.Alpha = _Saturate_0a167b172160490698449e5e903dd020_Out_1;
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
                float4 _Color;
                float _Gradient_Height;
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
                
                void Unity_Clamp_float(float In, float Min, float Max, out float Out)
                {
                    Out = clamp(In, Min, Max);
                }
                
                void Unity_Power_float(float A, float B, out float Out)
                {
                    Out = pow(A, B);
                }
                
                void Unity_OneMinus_float(float In, out float Out)
                {
                    Out = 1 - In;
                }
                
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
                
                void Unity_Saturate_float(float In, out float Out)
                {
                    Out = saturate(In);
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
                    UnityTexture2D _Property_e3f1be0cae404672b5b906572aa44bbd_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    float4 _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_e3f1be0cae404672b5b906572aa44bbd_Out_0.tex, _Property_e3f1be0cae404672b5b906572aa44bbd_Out_0.samplerstate, _Property_e3f1be0cae404672b5b906572aa44bbd_Out_0.GetTransformedUV(IN.uv0.xy));
                    float _SampleTexture2D_58daf8465f844985a17d31905c909f3e_R_4 = _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0.r;
                    float _SampleTexture2D_58daf8465f844985a17d31905c909f3e_G_5 = _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0.g;
                    float _SampleTexture2D_58daf8465f844985a17d31905c909f3e_B_6 = _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0.b;
                    float _SampleTexture2D_58daf8465f844985a17d31905c909f3e_A_7 = _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0.a;
                    float4 _Multiply_54c1b0eebb3641f899ac4a082ce86bf1_Out_2;
                    Unity_Multiply_float4_float4(IN.VertexColor, _SampleTexture2D_58daf8465f844985a17d31905c909f3e_RGBA_0, _Multiply_54c1b0eebb3641f899ac4a082ce86bf1_Out_2);
                    float4 _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0 = IN.uv0;
                    float _Split_0ad2ed23bf6a497183a7d660e97962bc_R_1 = _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0[0];
                    float _Split_0ad2ed23bf6a497183a7d660e97962bc_G_2 = _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0[1];
                    float _Split_0ad2ed23bf6a497183a7d660e97962bc_B_3 = _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0[2];
                    float _Split_0ad2ed23bf6a497183a7d660e97962bc_A_4 = _UV_9e2ee29fbc6848cfaee1e374f63c5136_Out_0[3];
                    float _Clamp_93a7abe938c34e4eb5db7f90bc5931a9_Out_3;
                    Unity_Clamp_float(_Split_0ad2ed23bf6a497183a7d660e97962bc_G_2, 0, 1, _Clamp_93a7abe938c34e4eb5db7f90bc5931a9_Out_3);
                    float _Property_3b3188b7a8d141a380525d46398c8f72_Out_0 = _Gradient_Height;
                    float _Power_629dbb45078f414baba7509e0113a79c_Out_2;
                    Unity_Power_float(_Clamp_93a7abe938c34e4eb5db7f90bc5931a9_Out_3, _Property_3b3188b7a8d141a380525d46398c8f72_Out_0, _Power_629dbb45078f414baba7509e0113a79c_Out_2);
                    float _OneMinus_4312b2145c684a10bc8a07ef92676a19_Out_1;
                    Unity_OneMinus_float(_Power_629dbb45078f414baba7509e0113a79c_Out_2, _OneMinus_4312b2145c684a10bc8a07ef92676a19_Out_1);
                    float _Multiply_970e04d2cf4c459e84bf9f5031cdfd80_Out_2;
                    Unity_Multiply_float_float(_SampleTexture2D_58daf8465f844985a17d31905c909f3e_A_7, _OneMinus_4312b2145c684a10bc8a07ef92676a19_Out_1, _Multiply_970e04d2cf4c459e84bf9f5031cdfd80_Out_2);
                    float _Saturate_0a167b172160490698449e5e903dd020_Out_1;
                    Unity_Saturate_float(_Multiply_970e04d2cf4c459e84bf9f5031cdfd80_Out_2, _Saturate_0a167b172160490698449e5e903dd020_Out_1);
                    surface.BaseColor = (_Multiply_54c1b0eebb3641f899ac4a082ce86bf1_Out_2.xyz);
                    surface.Alpha = _Saturate_0a167b172160490698449e5e903dd020_Out_1;
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