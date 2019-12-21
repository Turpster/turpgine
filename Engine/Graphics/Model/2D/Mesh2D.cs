using System;
using System.Runtime.InteropServices;
using Engine.Graphics.Scheduler;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Model._2D
{
    public class Mesh2D : Mesh
    {
        private Vertex2D[] _vertices;
        
        public Mesh2D(Vertex2D[] vertices, Model model, uint[] indices) : base(model, indices, 2)
        {
            _vertices = vertices
            #if DEBUG 
                    ?? throw new ArgumentNullException(nameof(vertices)); 
            #endif
        }

        protected override GlCallResult GlBufferData()
        {
            // TODO Might be able to optimise SizeOf.
            return GlRenderHandler.GlCallSync(() =>
            {
                GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf(_vertices), ref _vertices[0],
                    BufferUsageHint.StaticDraw); 
            });
        }
    }
}