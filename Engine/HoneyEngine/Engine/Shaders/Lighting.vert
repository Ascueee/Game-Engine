#version 330
layout (location = 0) in vec3 pos;
layout(location = 1) in vec2 texCoords;
layout (location = 2) in vec3 normals;
layout(location = 3) in vec3 instancePos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform mat3 inverseTranspose;


out vec3 normal;
out vec3 fragPos;
out vec2 texCoord;

void main()
{
    texCoord = texCoords;
    normal = inverseTranspose * normals;
    
    
    fragPos = vec3(model * vec4(pos + instancePos, 1.0));
    gl_Position = projection * view * model * vec4(pos + instancePos, 1.0);

}