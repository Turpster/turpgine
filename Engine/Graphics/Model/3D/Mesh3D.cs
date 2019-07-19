using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Model._3D
{
    public class Mesh3D : Mesh
    {
        public uint[] GlBuffers;

        public uint GlVao;

        public int NumVertices;

        readonly List<Vertex> Vertices = new List<Vertex>();
        
        public Mesh3D(List<Vertex> vertices)
        {
            Vertices = vertices;
        }

        public override void Render()
        {
            GL.BindVertexArray(GlVao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, NumVertices);
            GL.BindVertexArray(0);
        }

        protected bool Equals(Mesh3D other)
        {
            return Equals(GlBuffers, other.GlBuffers) && GlVao == other.GlVao && NumVertices == other.NumVertices;
        }

        public override void GlInit()
        {
            NumVertices = Vertices.Count;

            var positions = new Vector3[Vertices.Count];
            for (var i = 0; i < Vertices.Count; i++) positions[i] = Vertices[i].Position;

            GL.GenVertexArrays(1, out GlVao);
            GL.BindVertexArray(GlVao);

            var bufferLength = Enum.GetNames(typeof(VertexBuffer)).Length;
            GlBuffers = new uint[bufferLength];
            GL.GenBuffers(bufferLength, GlBuffers);

            GL.BindBuffer(BufferTarget.ArrayBuffer, GlBuffers[(int) VertexBuffer.Pos]);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf(typeof(Vertex)) * Vertices.Count, ref positions[0],
                BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray((int) VertexBuffer.Pos);
            GL.VertexAttribPointer((int) VertexBuffer.Pos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindVertexArray(0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Mesh3D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = GlBuffers != null ? GlBuffers.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (int) GlVao;
                hashCode = (hashCode * 397) ^ NumVertices;
                return hashCode;
            }
        }

        public static bool operator ==(Mesh3D mesh3D, Mesh3D targetMesh3D)
        {
            if (mesh3D == null || targetMesh3D == null)
                return false;

            return mesh3D.GlVao == targetMesh3D.GlVao;
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