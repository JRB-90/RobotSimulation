#version 120

varying vec3 positionOut;
varying vec3 normalOut;
varying vec4 colorOut;
varying vec2 texCoordOut;

//FRAG_OUT

uniform vec4 modelColor;

void main()
{
	gl_FragColor = modelColor;
}
