#version 450 core

uniform sampler2D diffuse;

void main()
{
    gl_FragColor = vec4(0.05f, 0.20f, 0.3f, 0.5f);
}