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
        public readonly Item items;

        public S_ShopInfoRes(Item items)
        {
            this.items = items;
        }

        public void Read(ArraySegment<byte> segment)
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
