#version 120

varying vec3 positionOut;
varying vec3 normalOut;
varying vec4 colorOut;
varying vec2 texCoordOut;

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

uniform vec4 ambientLight;
uniform LightSource light;
uniform MaterialData material;

vec3 scaleNorm(vec3 norm);

void main()
{
	vec4 ambient = material.ambient * ambientLight;
	vec4 diffuse = material.diffuse * light.color;
	vec3 lightDir = normalize(light.direction);
	vec3 flatNormal = normalize(cross(dFdx(positionOut), dFdy(positionOut)));
	float intensity = (-dot(flatNormal, lightDir) + 1) * 0.5;
	diffuse = diffuse * intensity;

	gl_FragColor = mix(ambient, diffuse, 1.0);
}
