#version 120

attribute vec3 positionIn;
attribute vec3 normalIn;
attribute vec4 colorIn;
attribute vec2 texCoordIn;

varying vec3 positionOut;
varying vec3 normalOut;
varying vec4 colorOut;
varying vec2 texCoordOut;

uniform mat4 modelMat;
uniform mat4 mvpMat;

void main()
{
	positionOut = vec4(modelMat * vec4(positionIn, 1.0)).xyz;
	normalOut = normalize(vec4(modelMat * vec4(normalIn, 0.0)).xyz);
	colorOut = colorIn;
	texCoordOut = texCoordIn;

	gl_Position = mvpMat * vec4(positionIn, 1.0);
}