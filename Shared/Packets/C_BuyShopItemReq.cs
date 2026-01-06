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
        public readonly ushort itemIndex;

        public C_BuyShopItemReq(ushort itemIndex)
        {
            this.itemIndex = itemIndex;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            throw new NotImplementedException();
        }

        public ArraySegment<byte> Write()
        {
            throw new NotImplementedException();
        }
    }
}
