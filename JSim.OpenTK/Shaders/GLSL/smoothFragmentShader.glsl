#version 120

varying vec3 positionOut;
varying vec3 normalOut;
varying vec4 colorOut;
varying vec2 texCoordOut;

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

uniform vec3 eyePosition;
uniform int activeLights;
uniform vec4 ambientLight;
uniform LightSource lights[MAX_LIGHTS];
uniform MaterialData material;

vec4 CalculateLight(LightSource light);

vec3 normalDirection;
vec3 viewDirection;

void main()
{
	normalDirection = normalize(normalOut);
	viewDirection = normalize(positionOut - eyePosition);

	vec4 totalLighting = material.ambient * ambientLight;

	for (int i = 0; i < activeLights; i++)
	{
		if (i < MAX_LIGHTS && lights[i].type == DIRECTIONAL_LIGHT)
		{
			totalLighting += CalculateLight(lights[i]);
		}
	}

	gl_FragColor = totalLighting;
}

vec4 CalculateLight(LightSource light)
{
	float totalAttenuation;
	vec3 lightDirection;
	vec3 diffuseOut;
	vec3 specularOut;

	if (light.type == DIRECTIONAL_LIGHT)
	{
		totalAttenuation = light.constantAttenuation;
		lightDirection = normalize(light.direction);
	}
	else
	{
		lightDirection = normalize(positionOut - light.position);
		float dist = length(lightDirection);

		totalAttenuation =
			light.constantAttenuation +
			(light.linearAttenuation * dist) +
			(light.quadraticAttenuation * dist * dist);

		totalAttenuation = 1.0 / totalAttenuation;

		if (light.type == POINT_LIGHT)
		{
		}
		else if (light.type == SPOT_LIGHT)
		{
			float clampedCosine = max(0.0, dot(lightDirection, normalize(light.direction)));

			if (clampedCosine < cos(radians(light.spotCutoff)))
			{
				totalAttenuation = 0.0;
			}
			else
			{
				totalAttenuation = totalAttenuation * pow(clampedCosine, light.spotExponent);
			}
		}
	}

	float normalToLightAngle = -dot(normalDirection, lightDirection);
	float diffuseIntensity = (normalToLightAngle + 1) * 0.5;
	//float diffuseIntensity = normalToLightAngle;

	diffuseOut =
		light.color.xyz *
		material.diffuse.xyz *
		totalAttenuation *
		diffuseIntensity;

	vec3 reflectDirection = normalize(reflect(lightDirection, normalDirection));
	float viewToLightAngle = -dot(viewDirection, reflectDirection);
	float specularIntensity = pow(max(0.0, viewToLightAngle), 1.0 / material.shininess);

	specularOut =
		light.color.xyz *
		material.specular.xyz *
		totalAttenuation *
		specularIntensity;

	return vec4(diffuseOut, 1.0) + vec4(specularOut, 1.0);
}