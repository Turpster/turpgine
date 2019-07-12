using System;
using System.Collections.Generic;
using Engine.Entity;
using Engine.Shader;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;
using LoggerLite;

namespace engine
{
    public class Engine
    {
        static Engine engine;
        
        public GameWindow window;

        public Engine()
        {
            window = new GameWindow(600, 600, null, "Hello World");
            
            GraphicsContext.CurrentContext.ErrorChecking = true;
            
            ShaderManager shaderManager = new ShaderManager( 
                
                new Shader("/home/turpster/Develop/Git/open-gl/Engine/Shader/GLSL/vertex-shader.vert", ShaderType.VertexShader),
                new Shader("/home/turpster/Develop/Git/open-gl/Engine/Shader/GLSL/fragment-shader.frag", ShaderType.FragmentShader)
                
                );
            
            shaderManager.Use();
            
            List<Vertex> vertices = new List<Vertex>();
            
            vertices.Add(new Vertex(new Vector3(0.5f, -0.5f, 1.0f)));
            vertices.Add(new Vertex(new Vector3(0.0f, 0.5f, 1.0f)));
            vertices.Add(new Vertex(new Vector3(-0.5f, -0.5f, 1.0f)));

            Mesh mesh = new Mesh(vertices);
            
            GL.ClearColor(0.05f, 0.15f, 0.3f, 1.0f);
            
            window.RenderFrame += (w, e) =>
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);

                mesh.Render();
                
                window.SwapBuffers();
                
                window.ProcessEvents();
            }; 
                
            window.Run();
        }

    }
}
