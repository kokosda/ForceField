
sampler2D tex : register(s0);

float4 PixelShaderFunction(float2 tex2co : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    return tex2D(tex,tex2co) * color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
