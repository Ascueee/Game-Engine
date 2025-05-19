#version 330

struct material {
    sampler2D diffuse;
    sampler2D normal;
    vec3 specular;
    float shininess;
};

struct light {
    vec3 lightPos;
    
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform vec3 shapeColor;
uniform vec3 viewPos;
uniform material mat;
uniform light lightObj;

in vec3 normal;
in vec3 fragPos;
in vec2 texCoord;
out vec4 fragColor;

void main() {
    vec3 ambient = lightObj.ambient * vec3(texture(mat.diffuse, texCoord));
    
    vec3 sampledNormal = texture(mat.normal, texCoord).rgb;
    vec3 norm = normalize(sampledNormal * 2.0 - 1.0);
    
    vec3 lightDir = normalize(lightObj.lightPos - fragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = lightObj.diffuse * (vec3(texture(mat.diffuse, texCoord)) * shapeColor);
    
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), mat.shininess);
    vec3 specular = (mat.specular * spec) * lightObj.diffuse;

    vec3 lightingResult = ambient + diffuse + specular;
    fragColor = vec4(lightingResult, 1);
}