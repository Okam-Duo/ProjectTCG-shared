using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Packets
{
    /// <summary>
    /// 상점에 제시된 제품 정보 요청
    /// </summary>
    public struct C_ShopInfoReq : Network.IPacket
    {
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
