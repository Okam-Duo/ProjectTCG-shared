using System;

namespace Shared.Packets
{
    public interface ISerializableData
    {
        public void Read(in ArraySegment<byte> segment, ref int count);
        public bool Write(ArraySegment<byte> segment, ref int count);
    }
}
