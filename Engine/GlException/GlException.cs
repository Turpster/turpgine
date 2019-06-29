using System;
using System.Text;

namespace Engine.GlException
{
    public class GlException : Exception
    {
        public override string Message { get; }

        public GlException(string cause, string glErrorMessage)
        {
            StringBuilder exceptionErrorMessage = new StringBuilder("There was an exception caused by OpenGL.\n" +
                                                                    $"OpenGL Cause: {cause}\n");

            if (glErrorMessage != null)
            {
                exceptionErrorMessage.Append($"OpenGL Message: {glErrorMessage}");
            }

            Message = exceptionErrorMessage.ToString();
        }
        
        public GlException(string cause) : base(cause, null) {}
    }
}