#version 120

varying vec3 position0;
varying vec3 normal0;
varying vec4 color0;
varying vec2 texCoord0;

struct LightSource
{
	vec4 position;
	vec4 color;
	vec3 direction;
	float constantAttenuation, linearAttenuation, quadraticAttenuation;
	float spotCutoff, spotExponent;
};

struct MaterialData
{
	vec4 ambient;
	vec4 diffuse;
	vec4 specular;
	float shininess;
};

uniform sampler2D sampler;
uniform LightSource light;
uniform MaterialData material;

void main()
{
	vec4 ambient = material.ambient * light.color;
	vec4 diffuse = material.diffuse;

	vec4 texColor = texture2D(sampler, texCoord0.xy).rgba;
	if (texColor != vec4(0.0, 0.0, 0.0, 1.0))
	{
		ambient = texColor * 0.4;
		diffuse = texColor;
	}

	vec3 flatNormal = normalize(cross(dFdx(position0), dFdy(position0)));
	vec3 lightDir = -normalize(light.direction);
	//float intensity = dot(lightDir, flatNormal);
    float intensity = max(dot(flatNormal, lightDir), 0.0);
	vec4 diffuse0 = light.color * (diffuse * intensity);

	gl_FragColor = ambient + diffuse0;
	//gl_FragColor = light.color;
	//gl_FragColor = vec4(flatNormal, 1.0);
}