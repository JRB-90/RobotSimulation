#version 120

varying vec3 position0;
varying vec3 normal0;
varying vec4 color0;
varying vec2 texCoord0;

uniform sampler2D sampler;
uniform vec4 newColor;

void main()
{
	vec4 baseColor;
	if (color0.w >= 0)
	{
		baseColor = color0;
	}
	else
	{
		baseColor = newColor;
	}

	//vec4 texColor = texture2D(sampler, texCoord0.xy).rgba;
	//if (texColor != vec4(0.0, 0.0, 0.0, 1.0))
	//{
	//	baseColor = texColor;
	//}

	baseColor = newColor;
	gl_FragColor = baseColor;
}
