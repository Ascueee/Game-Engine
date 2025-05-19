#version 330

uniform vec3 shapeColor;

out vec4 fragColor;

void main() {
    fragColor = vec4(shapeColor, 1);
}