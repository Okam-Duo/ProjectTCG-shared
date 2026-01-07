using Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Packets
{
    /// <summary>
    /// 상점의 특정 제품 구매 요청
    /// </summary>
    public struct C_BuyShopItemReq : Network.IPacket
    {
        /// <summary>
        /// ShopInfoRes로 받은 아이템중 몇번째를 구매했는지
        /// </summary>
        public ushort itemIndex;

        public PacketID PacketID => PacketID.C_BuyShopItemReq;

        public void Read(in ArraySegment<byte> segment)
        {
            itemIndex = BitConverter.ToUInt16(segment);
        }

        int IPacket.Write(in ArraySegment<byte> s)
        {
            BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), itemIndex);

            return sizeof(ushort);
        }
    }
}
