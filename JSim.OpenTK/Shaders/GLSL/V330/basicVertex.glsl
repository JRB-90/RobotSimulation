#version 330

layout (location = 0) in vec3 positionIn;
layout (location = 1) in vec3 normalIn;
layout (location = 2) in vec4 colorIn;
layout (location = 3) in vec2 texCoordIn;

out vec3 positionOut;
out vec3 normalOut;
out vec4 colorOut;
out vec2 texCoordOut;

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