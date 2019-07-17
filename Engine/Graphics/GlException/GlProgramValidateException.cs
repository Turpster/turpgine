namespace Engine.Graphics.GlException
{
    public class GlProgramValidateException : GlException
    {
        public GlProgramValidateException(string glErrorMessage) : base("Linking a program", glErrorMessage)
        {
        }

        public override string Message { get; }
    }
}