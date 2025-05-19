#version 330
layout (location = 0) in vec3 pos;
layout(location = 1) in vec3 instancePos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = vec4(pos + instancePos,1) * model * view * projection;
}
