using System;

namespace engine
{
    public class GlException : Exception
    {
        public override string Message { get; }

        public string GlCause { get; }
        
        public GlException(string glCause, string errorMessage)
        {
            Message = "There was an exception caused by OpenGL.\n" +
                      $"OpenGL Cause: {GlCause}\n" +
                      $"OpenGL Message: {errorMessage}";
        }
    }
}