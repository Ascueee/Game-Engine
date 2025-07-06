#version 330
layout (location = 0) in vec3 pos;
layout(location = 1) in vec3 instancePos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
out vec3 worldPos;
void main()
{
    worldPos = vec3(model * vec4(pos + instancePos, 1.0));
    
    gl_Position = projection * view * model * vec4(pos + instancePos, 1.0);
}