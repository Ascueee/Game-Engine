#version 330
layout(location = 0) in vec3 pos;

uniform mat4 view;
uniform mat4 projection;

out vec3 texCoords;

void main() {
    texCoords = pos;
    vec4 pos2 = vec4(pos, 1.0) * projection * view;
    gl_Position = pos2.xyww; 
}