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

        public S_BuyShopItemRes(bool isSuccess, Item[] rewards)
        {
            this.isSuccess = isSuccess;
            this.rewards = rewards;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            throw new NotImplementedException();
        }

        public ArraySegment<byte> Write()
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
