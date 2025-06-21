#version 330
struct material {
    sampler2D diffuse;
    sampler2D normal;
    vec3 specular;
    float shininess;
};

struct DirLight{
    vec3 direction;
    
    vec3 color;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
struct PointLight {
    vec3 position;

    float constant;
    float linear;
    float quadratic;
    
    vec3 color;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
#define numOfPointLights 2
uniform PointLight pointLights[numOfPointLights];
vec3 CalculateDirectionalLight(DirLight light, vec3 normal, vec3 viewDir);
vec3 CalculatePointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir);

uniform vec3 shapeColor;
uniform vec3 viewPos;
uniform material mat;


uniform DirLight dirLight;

in vec3 normal;
in vec3 fragPos;
in vec2 texCoord;
out vec4 fragColor;

void main() {
    
    vec3 sampledNormal = texture(mat.normal, texCoord).rgb;
    vec3 norm = normalize(sampledNormal * 2.0 - 1.0);
    
    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 lightingResult = CalculateDirectionalLight(dirLight, norm, viewDir);
    
    for(int i = 0; i < numOfPointLights; i++){
        lightingResult += CalculatePointLight(pointLights[i], norm, fragPos, viewDir);
    }
    
    fragColor = vec4(lightingResult, 1);
}

vec3 CalculateDirectionalLight(DirLight light, vec3 normal, vec3 viewDir){
    vec3 lightDir = normalize(-light.direction);
    float diff = max(dot(normal, lightDir), 0.0);
    
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), mat.shininess);
    
    vec3 texColor = vec3(texture(mat.diffuse, texCoord));
    vec3 ambient = light.ambient * light.color * texColor;
    vec3 diffuse = light.diffuse * light.color * diff * texColor;
    vec3 specular = light.specular * light.color * spec * mat.specular;
    
    return (ambient + diffuse + specular);
}

vec3 CalculatePointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir){
    vec3 lightDir = normalize(light.position - fragPos);
    
    float diff = max(dot(normal, lightDir), 0.0);
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), mat.shininess);

    float distance    = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance +
    light.quadratic * (distance * distance));

    vec3 texColor = vec3(texture(mat.diffuse, texCoord));
    vec3 ambient = light.ambient * light.color * texColor;
    vec3 diffuse = light.diffuse * light.color  * diff * texColor;
    vec3 specular = light.specular * light.color * spec * mat.specular;

    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;
    
    return (ambient + diffuse + specular);
    
}