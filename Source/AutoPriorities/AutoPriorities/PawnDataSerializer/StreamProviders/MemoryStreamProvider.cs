using System;
using System.IO;

namespace AutoPriorities.PawnDataSerializer.StreamProviders
{
    internal class MemoryStreamProvider : StreamProvider
    {
        private readonly MemoryStream _stream;

        public MemoryStreamProvider(MemoryStream stream)
        {
            _stream = stream;
        }

        public override T WithStream<T>(string path, FileMode mode, Func<Stream, T> callback)
        {
            return callback(_stream);
        }

        public override bool FileExists(string path)
        {
            return true;
        }
    }
}
