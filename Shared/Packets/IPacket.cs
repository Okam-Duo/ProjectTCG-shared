using System;

namespace Shared.Packets
{
    public interface IPacket
    {
        //public PacketID PacketID { get; }

        public void Read(in ArraySegment<byte> segment);
        /// <param name="s">SendBuffer에 할당되어있는 공간</param>
        /// <returns>Span에서 사용한 공간의 크기</returns>
        public ArraySegment<byte> Write();
    }
}