﻿#version 120

attribute vec3 position;
attribute vec3 normal;
attribute vec4 color;
attribute vec2 texCoord;

varying vec3 position0;
varying vec3 normal0;
varying vec4 color0;
varying vec2 texCoord0;

//uniform mat4 modelMat;
uniform mat4 MVPMat;

void main()
{
	//position0 = vec4(modelMat * vec4(position, 1.0)).xyz;
	position0 = position;
	//normal0 = normalize(vec4(modelMat * vec4(normal, 0.0)).xyz);
	normal0 = normal;
	color0 = color;
	texCoord0 = texCoord;

	gl_Position = MVPMat * vec4(position, 1.0);

	//gl_Position = vec4(position, 1.0);
}
