using Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Packets
{
    /// <summary>
    /// 서버에 어떤 자원이 얼마나 있는지 물어보기
    /// </summary>
    public struct C_ResourceInfoReq : Network.IPacket
    {
        /// <summary>
        /// 서버에 물어볼 자원 타입
        /// </summary>
        public readonly ResourceType[] resourceType;

        void IPacket.Read(in ArraySegment<byte> segment)
        {
            throw new NotImplementedException();
        }

        int IPacket.Write(in ArraySegment<byte> s)
        {
            throw new NotImplementedException();
        }
    }
}
