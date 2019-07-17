using System;
using System.Text;

namespace Engine.Graphics.GlException
{
    public class GlException : Exception
    {
        public GlException(string cause, string glErrorMessage = null) : base(GetErrorMessage(cause, glErrorMessage))
        {
        }

        private static string GetErrorMessage(string cause, string glErrorMessage)
        {
            var exceptionErrorMessage = new StringBuilder("There was an exception caused by OpenGL.\n" +
                                                          "OpenGL Cause: " + cause + "\n");

            if (glErrorMessage != null) exceptionErrorMessage.Append($"OpenGL Message: {glErrorMessage}\n");

            return exceptionErrorMessage.ToString();
        }
    }
}