using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Entity
{
    public class Mesh
    {
        enum VertexBuffer
        {
            POS=0
        }
        
        public uint gl_vao;
        public uint[] gl_buffers;

        public int num_vertices;

        public Mesh(List<Vertex> vertices)
        {
            num_vertices = vertices.Count;
            
            GL.GenVertexArrays(1, out gl_vao);
            GL.GenVertexArrays(1, gl_buffers);

            Vector3[] Positions = new Vector3[vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
            {    
                Positions[i] = vertices[i].Position;
            }
            
            GL.BindVertexArray(gl_vao);

            int buffer_length = Enum.GetNames(typeof(VertexBuffer)).Length;

            gl_buffers = new uint[buffer_length];
            GL.GenBuffers(buffer_length, gl_buffers);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, gl_buffers[(int) VertexBuffer.POS]);
            
            GL.BufferData(BufferTarget.ArrayBuffer,  Marshal.SizeOf( typeof(Vertex)) * vertices.Count,  ref Positions[0], BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray((int) VertexBuffer.POS);
            GL.VertexAttribPointer((int) VertexBuffer.POS, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindVertexArray(0);
        }

        public void Render()
        {
            GL.BindVertexArray(gl_vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, num_vertices);
            GL.BindVertexArray(0);
        }
    }
}