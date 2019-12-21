using System;
using System.Runtime.InteropServices;
using Engine.Graphics.Scheduler;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics.Vulkan;

namespace Engine.Graphics.Model._3D
{
    public class Mesh3D : Mesh
    {
        private Vertex3D[] _vertices;

        public Mesh3D(Model model, Vertex3D[] vertices, uint[] indices) : base(model, indices, 3)
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