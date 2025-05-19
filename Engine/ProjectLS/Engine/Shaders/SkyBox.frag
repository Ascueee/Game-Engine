#version 330
uniform samplerCube skyBox;

in vec3 texCoords;
out vec4 fragColor;

void main() {

    fragColor = texture(skyBox, texCoords);
}