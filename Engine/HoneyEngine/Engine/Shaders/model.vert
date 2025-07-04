#version 330
layout(location = 0) in vec3 pos;
layout(location = 1) in vec2 texCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec2 texCoords;

void main() {
    gl_Position = vec4(pos,1) * model * view * projection;
    texCoords = texCoord;
}