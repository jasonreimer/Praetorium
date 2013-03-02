using System.IO;

namespace Praetorium
{
    public static class IOExtensions
    {

        public static TextWriter WriteIf(this TextWriter writer, bool condition, string format, params object[] args)
        {
            Ensure.ArgumentNotNull(() => writer);

            if (condition)
                writer.Write(format, args);

            return writer;
        }

        public static TextWriter WriteLineIf(this TextWriter writer, bool condition, string format, params object[] args)
        {
            Ensure.ArgumentNotNull(() => writer);

            if (condition)
                writer.WriteLine(format, args);

            return writer;
        }

        public static string ReadAllText(this Stream stream)
        {
            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        /// <summary>
        /// Reads the <paramref name="stream"/> from the current position to the end of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns>
        /// Returns all bytes read.
        /// </returns>
        public static byte[] ReadToEnd(this Stream stream, int bufferSize = 4096)
        {
            Ensure.ArgumentNotNull(() => stream);

            using (var output = new MemoryStream())
            {
                var buffer = new byte[bufferSize];
                int read = stream.Read(buffer, 0, 4096);

                while (read > 0)
                {
                    output.Write(buffer, 0, read);

                    read = stream.Read(buffer, 0, 4096);
                }

                return output.ToArray();
            }
        }
    }
}
