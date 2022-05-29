#version 330

in vec3 positionOut;
in vec3 normalOut;
in vec4 colorOut;
in vec2 texCoordOut;

out vec4 fragColor;

const int MAX_LIGHTS = 8;
const int DIRECTIONAL_LIGHT = 1;
const int POINT_LIGHT = 2;
const int SPOT_LIGHT = 3;

struct LightSource
{
	int type;
	vec4 color;
	vec3 position;
	vec3 direction;
	float constantAttenuation;
	float linearAttenuation;
	float quadraticAttenuation;
	float spotCutoff;
	float spotExponent;
};

struct MaterialData
{
	vec4 ambient;
	vec4 diffuse;
	vec4 specular;
	float shininess;
};

uniform int activeLights;
uniform vec4 ambientLight;
uniform LightSource lights[MAX_LIGHTS];
uniform MaterialData material;

vec4 CalculateLight(LightSource light);

void main()
{
	vec4 totalLighting = material.ambient * ambientLight;

	for (int i = 0; i < activeLights; i++)
	{
		if (i < MAX_LIGHTS &&
			lights[i].type == DIRECTIONAL_LIGHT)
		{
			totalLighting += CalculateLight(lights[i]);
		}
	}

	fragColor = totalLighting;
}

vec4 CalculateLight(LightSource light)
{
	vec4 diffuseOut = material.diffuse * light.color;

	vec3 lightDir = normalize(light.direction);

	vec3 flatNormal = 
		normalize(
			cross(
				dFdx(positionOut),
				dFdy(positionOut)
			)
		);

	float intensity = 
		(-dot(flatNormal, lightDir) + 1) * 
		0.5;

	return diffuseOut * intensity * light.constantAttenuation;
}
