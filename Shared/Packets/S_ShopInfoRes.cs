using Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Packets
{
    /// <summary>
    /// 상점에 제시된 제품들 알려주기
    /// </summary>
    public struct S_ShopInfoRes : Network.IPacket
    {
        /// <summary>
        /// 상점에 제시된 제품들
        /// </summary>
        public readonly Item[] items;

        public PacketID PacketID => PacketID.S_ShopInfoRes;

        void IPacket.Read(in ArraySegment<byte> segment)
        {
            throw new NotImplementedException();
        }

        int IPacket.Write(in ArraySegment<byte> s)
        {
            throw new NotImplementedException();
        }


        public struct Item
        {
            public ResourceType resourceType;
            public ushort id;
            public ushort quantity;
        }
    }
}
