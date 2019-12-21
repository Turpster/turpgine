using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Engine.Graphics.Scheduler;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Model
{
    public abstract class Mesh : GlObject, IGlRenderable
    {
        
        protected internal Model Model;
        private int _dimensions;

        private uint[] _indices;
        private int _numIndices;

        public uint[] GlBuffers;
        public uint GlElementBuffer;
        public uint GlVao;

        public int[] Vertices
        {
            get
            {
                // TODO Get vertices from OpenGL.
                throw new NotImplementedException();
            }
        }
        
        public int[] Indices
        {
            get
            {
                // TODO Get indices from OpenGL.
                throw new NotImplementedException();
            }
        }

        internal Mesh(Model model, uint[] indices, int dimensions) : base(model._modelManager)
        {
            _dimensions = dimensions;
            Model = model;
            
            _indices = indices 
            #if DEBUG 
                ?? throw new ArgumentNullException(nameof(indices));
            #endif
        }

        public override GlCallResult _glInitialise()
        {
            return GlRenderHandler.GlCall(() =>
            {
                GL.GenVertexArrays(1, out GlVao);
                GL.BindVertexArray(GlVao);

                // Element Buffer
                GL.GenBuffers(1, out GlElementBuffer);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, GlElementBuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * Marshal.SizeOf(typeof(uint)), 
                    _indices, BufferUsageHint.StaticDraw);
                _numIndices = _indices.Length;
                _indices = null;

                // Vertex Buffers
                var bufferLength = Enum.GetNames(typeof(VertexBuffer)).Length;
                GlBuffers = new uint[bufferLength];
                GL.GenBuffers(bufferLength, GlBuffers);

                GL.BindBuffer(BufferTarget.ArrayBuffer, GlBuffers[(int) VertexBuffer.Pos]);
                
                #if DEBUG
                var result = GlBufferData();
                if (!result.Synchronised)
                {
                    throw new InvalidAsynchronousStateException("GlBufferData() should be executing synchronised.");
                }
                #else
                GlBufferData();
                #endif

                GL.EnableVertexAttribArray((int) VertexBuffer.Pos);
                GL.VertexAttribPointer((int) VertexBuffer.Pos, _dimensions, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindVertexArray(0);
            });
        }

        
        // GlCallResult must execute synchronised.
        protected abstract GlCallResult GlBufferData();

        public override GlCallResult _glDispose()
        {
            return GlRenderHandler.GlCall(() => 
            {
                GL.DeleteBuffers(1, ref GlElementBuffer);
                GL.DeleteBuffers(Enum.GetNames(typeof(VertexBuffer)).Length, GlBuffers);
            
                GL.DeleteVertexArray(GlVao);
            });
        }

        public GlCallResult GlRender()
        {
            return GlRenderHandler.GlCall(() => 
            {
                GL.BindVertexArray(GlVao);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, GlElementBuffer);
                GL.DrawElements(PrimitiveType.Triangles, _numIndices, DrawElementsType.UnsignedInt, 0);
                GL.BindVertexArray(0);
            }, true);
        }

        protected bool Equals(Mesh other)
        {
            return Equals(GlBuffers, other.GlBuffers) && GlVao == other.GlVao && _indices.Length == other._indices.Length;
        }
        
        private enum VertexBuffer
        {
            Pos = 0
        }
        
        public static bool operator ==(Mesh mesh, Mesh targetMesh)
        {
            if (!(mesh is null) && !(targetMesh is null))
                if (mesh.GlVao == targetMesh.GlVao)
                    return true;

            return ReferenceEquals(mesh, targetMesh);
        }


        public static bool operator !=(Mesh mesh, Mesh targetMesh)
        {
            return !(mesh == targetMesh);
        }
    }
}