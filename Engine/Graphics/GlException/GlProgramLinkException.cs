namespace Engine.Graphics.GlException
{
    public class GlProgramLinkException : GlException
    {
        public GlProgramLinkException(string glErrorMessage) : base("Linking a program", glErrorMessage)
        {
        }

        public override string Message { get; }
    }
}