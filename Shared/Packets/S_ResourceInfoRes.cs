using Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Packets
{
    /// <summary>
    /// 유저의 자원 정보 알려주기
    /// </summary>
    public struct S_ResourceInfoRes : Network.IPacket
    {
        public ResourceInfo[] infos;

        public PacketID PacketID => PacketID.S_ResourceInfoRes;

        void IPacket.Read(in ArraySegment<byte> segment)
        {
            throw new NotImplementedException();
        }

        int IPacket.Write(in ArraySegment<byte> s)
        {
            throw new NotImplementedException();
        }


        public struct ResourceInfo
        {
            public ushort id;
            public ResourceType resourceType;
            public int count;
        }
    }
}
