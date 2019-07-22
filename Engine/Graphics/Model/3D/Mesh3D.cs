using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Model._3D
{
    public class Mesh3D : Mesh
    {
        public uint[] GlBuffers;

        public uint GlVao;
        public uint GlElementBuffer;
        
        private readonly Vertex[] Vertices;
        private readonly uint[] Indices;

        public Mesh3D(Vertex[] vertices, uint[] indices)
        {
            Vertices = vertices;
            Indices = indices;
        }

        public override void Render()
        {
            GL.BindVertexArray(GlVao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, GlElementBuffer);
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        protected bool Equals(Mesh3D other)
        {
            return Equals(GlBuffers, other.GlBuffers) && GlVao == other.GlVao && Indices.Length == other.Indices.Length;
        }

        protected internal override void GlInitialise()
        {
            var positions = new Vector3[Vertices.Length];
            
            for (var i = 0; i < Vertices.Length; i++) 
            {
                positions[i] = Vertices[i].Position;
            }

            GL.GenVertexArrays(1, out GlVao);
            GL.BindVertexArray(GlVao);

            // Element Buffer
            GL.GenBuffers(1, out GlElementBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, GlElementBuffer);

            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * Marshal.SizeOf(typeof(uint)), Indices,
                BufferUsageHint.StaticDraw);

            // Vertex Buffers
            var bufferLength = Enum.GetNames(typeof(VertexBuffer)).Length;
            GlBuffers = new uint[bufferLength];
            GL.GenBuffers(bufferLength, GlBuffers);

            GL.BindBuffer(BufferTarget.ArrayBuffer, GlBuffers[(int) VertexBuffer.Pos]);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf(typeof(Vector3)) * Indices.Length, ref positions[0],
                BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray((int) VertexBuffer.Pos);
            GL.VertexAttribPointer((int) VertexBuffer.Pos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindVertexArray(0);
        }

        protected internal override void GlTerminate()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            
            return Equals((Mesh3D) obj);
        }

        public static bool operator ==(Mesh3D mesh3D, Mesh3D targetMesh3D)
        {
            if (!ReferenceEquals(mesh3D, null) && !ReferenceEquals(targetMesh3D, null))
            {
                if (mesh3D.GlVao == targetMesh3D.GlVao)
                {
                    return true;
                }
            }

            return ReferenceEquals(mesh3D, targetMesh3D);
        }
        

        public static bool operator !=(Mesh3D mesh3D, Mesh3D targetMesh3D)
        {
            return !(mesh3D == targetMesh3D);
        }

        private enum VertexBuffer
        {
            Pos = 0
        }
    }
}