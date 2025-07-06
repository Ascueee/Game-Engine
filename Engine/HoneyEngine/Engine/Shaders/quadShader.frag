#version 330
uniform sampler2D baseTex;
uniform vec2 animationOffSet;
in vec2 texCoords;
out vec4 fragColor;

void main() {
    vec4 texColor = texture(baseTex, texCoords + animationOffSet);
    if(texColor.a < 0.1)
        discard;
    fragColor = texColor;
}