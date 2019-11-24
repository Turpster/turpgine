using System;
using System.Runtime.InteropServices;
using Logger;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Model._3D
{
    public class Mesh3D : Mesh
    {
        private uint[] _indices;
        private int _numIndices;
        
        private Vertex[] _vertices;

        public int[] Indices
        {
            get
            {
                // TODO Get indices from OpenGL.
                throw new NotImplementedException();
            }
        }
        
        public int[] Vertices
        {
            get
            {
                // TODO Get vertices from OpenGL.
                throw new NotImplementedException();
            }
        }

        public uint[] GlBuffers;
        public uint GlElementBuffer;

        public uint GlVao;

        public Mesh3D(Vertex[] vertices, uint[] indices)
        {
            _vertices = vertices;
            _indices = indices;
        }

        public override void Render()
        {
            GL.BindVertexArray(GlVao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, GlElementBuffer);
            GL.DrawElements(PrimitiveType.Triangles, _numIndices, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        protected bool Equals(Mesh3D other)
        {
            return Equals(GlBuffers, other.GlBuffers) && GlVao == other.GlVao && _indices.Length == other._indices.Length;
        }

        protected internal override void _glInitialise()
        {
            var positions = new Vector3[_vertices.Length];

            for (var i = 0; i < _vertices.Length; i++) positions[i] = _vertices[i].Position;

            GL.GenVertexArrays(1, out GlVao);
            GL.BindVertexArray(GlVao);

            // Element Buffer
            GL.GenBuffers(1, out GlElementBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, GlElementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * Marshal.SizeOf(typeof(uint)), _indices,
                BufferUsageHint.StaticDraw);
            _numIndices = _indices.Length;
            _indices = null;

            // Vertex Buffers
            var bufferLength = Enum.GetNames(typeof(VertexBuffer)).Length;
            GlBuffers = new uint[bufferLength];
            GL.GenBuffers(bufferLength, GlBuffers);

            GL.BindBuffer(BufferTarget.ArrayBuffer, GlBuffers[(int) VertexBuffer.Pos]);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf(typeof(Vector3)) * _vertices.Length, ref positions[0],
                BufferUsageHint.StaticDraw);
            _vertices = null;
            
            GL.EnableVertexAttribArray((int) VertexBuffer.Pos);
            GL.VertexAttribPointer((int) VertexBuffer.Pos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindVertexArray(0);
        }

        protected internal override void _glDispose()
        {
            GL.DeleteBuffers(1, ref GlElementBuffer);
            GL.DeleteBuffers(Enum.GetNames(typeof(VertexBuffer)).Length, GlBuffers);
            
            GL.DeleteVertexArray(GlVao);
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
                if (mesh3D.GlVao == targetMesh3D.GlVao)
                    return true;

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