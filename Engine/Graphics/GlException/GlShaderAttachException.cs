using System;

namespace Engine.GlException
{
    public class GlShaderAttachException : GlException
    {
        public override string Message { get; }

        public GlShaderAttachException(String cause) : base(cause) { }
    }
}