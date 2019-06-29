namespace Engine.GlException
{
    public class GlProgramLinkException : GlException
    {
        public override string Message { get; }

        public GlProgramLinkException(string glErrorMessage) : base("Linking a program", glErrorMessage) { }
    }
}