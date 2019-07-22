using System.IO;

namespace Common
{
    public static class StreamUtil
    {
        public static string ReadStringStream(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}