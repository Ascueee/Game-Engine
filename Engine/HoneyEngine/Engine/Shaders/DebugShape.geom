layout(points, invocations = 1) in;
layout(line_strip, max_vertices = 24) out;

uniform mat4 projection;
uniform mat4 view;
in vec3 worldPos[];

void emitLine(vec3 a, vec3 b)
{
    gl_Position = projection * view * vec4(a, 1.0);
    EmitVertex();
    gl_Position = projection * view * vec4(b, 1.0);
    EmitVertex();
    EndPrimitive();
}

void main()
{
    vec3 p[8];
    
    for (int i = 0; i < 8; ++i)
        p[i] = gl_in[i].gl_Position.xyz;

    emitLine(p[0], p[1]);
    emitLine(p[1], p[2]);
    emitLine(p[2], p[3]);
    emitLine(p[3], p[0]);

    emitLine(p[4], p[5]);
    emitLine(p[5], p[6]);
    emitLine(p[6], p[7]);
    emitLine(p[7], p[4]);

    emitLine(p[0], p[4]);
    emitLine(p[1], p[5]);
    emitLine(p[2], p[6]);
    emitLine(p[3], p[7]);
}