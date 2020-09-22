using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Network
{
    class BufferedStream : Stream
    {
        internal bool WriteImmediately { get; set; }
        public Stream Parent { get; set; } 
        public MemoryStream PendingStream { get; set; }

        public BufferedStream(Stream parent)
        {
            this.Parent = parent;
            PendingStream = new MemoryStream(512);
            WriteImmediately = false;
        }

        public override bool CanRead => Parent.CanRead;

        public override bool CanSeek => Parent.CanSeek;

        public override bool CanWrite => Parent.CanWrite;

        public override long Length => Parent.Length;

        public override long Position { get => Parent.Position; set => Parent.Position = value; }

        public override void Flush()
        {
            Parent.Write(PendingStream.GetBuffer(), 0, (int)PendingStream.Position);
            PendingStream.Position = 0;
        }

        public long PendingWrites => PendingStream.Position;

        public override int Read(byte[] buffer, int offset, int count)
        {
            return Parent.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return Parent.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            Parent.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (WriteImmediately)
                Parent.Write(buffer, offset, count);
            else
                PendingStream.Write(buffer, offset, count);
        }
    }
}
