namespace Engine.GlException
{
    public class GlProgramValidateException : GlException
    {
        public override string Message { get; }

        public GlProgramValidateException(string glErrorMessage) : base("Linking a program", glErrorMessage) { }
    }
}