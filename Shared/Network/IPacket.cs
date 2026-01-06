using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Network
{
    public interface IPacket
    {
        public static ArraySegment<byte> WriteHelper(IPacket packet)
        {
            ArraySegment<byte> buffer = SendBufferHelper.Open(4096);

            return SendBufferHelper.Close(packet.Write(buffer));
        }

        public void Read(in ArraySegment<byte> segment);
        /// <param name="s">SendBuffer에 할당되어있는 공간</param>
        /// <returns>Span에서 사용한 공간의 크기</returns>
        protected int Write(in ArraySegment<byte> s);
    }
}
