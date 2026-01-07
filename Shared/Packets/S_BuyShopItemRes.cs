using Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Packets
{
    /// <summary>
    /// 상점에서 제품을 구매한 결과
    /// </summary>
    public struct S_BuyShopItemRes : Network.IPacket
    {
        /// <summary>
        /// 구매 성공 여부
        /// </summary>
        public readonly bool isSuccess;
        /// <summary>
        /// 구매 결과
        /// </summary>
        public readonly Item[] rewards;

        public PacketID PacketID => PacketID.S_BuyShopItemRes;

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
