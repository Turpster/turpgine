using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.GameObject
{
    public class Mesh
    {
        enum VertexBuffer
        {
            Pos=0
        }
        
        public uint GlVao;
        public uint[] GlBuffers;

        public int NumVertices;

        public Mesh(List<Vertex> vertices)
        {
            NumVertices = vertices.Count;
            
            GL.GenVertexArrays(1, out GlVao);
            GL.GenVertexArrays(1, GlBuffers);

            Vector3[] positions = new Vector3[vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
            {    
                positions[i] = vertices[i].Position;
            }
            
            GL.BindVertexArray(GlVao);

            int bufferLength = Enum.GetNames(typeof(VertexBuffer)).Length;

            GlBuffers = new uint[bufferLength];
            
            GL.GenBuffers(bufferLength, GlBuffers);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, GlBuffers[(int) VertexBuffer.Pos]);
            
            GL.BufferData(BufferTarget.ArrayBuffer,  Marshal.SizeOf( typeof(Vertex)) * vertices.Count,  ref positions[0], BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray((int) VertexBuffer.Pos);
            GL.VertexAttribPointer((int) VertexBuffer.Pos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindVertexArray(0);
        }

        public void Render()
        {
            GL.BindVertexArray(GlVao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, NumVertices);
            GL.BindVertexArray(0);
        }
    }
}