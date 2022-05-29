#version 330

in vec3 positionOut;
in vec3 normalOut;
in vec4 colorOut;
in vec2 texCoordOut;

out vec4 fragColor;

uniform vec4 modelColor;

void main()
{
	fragColor = modelColor;
}
