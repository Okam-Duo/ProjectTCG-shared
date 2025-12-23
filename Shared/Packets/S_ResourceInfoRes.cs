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
        /// <summary>
        /// id, 보유량
        /// </summary>
        KeyValuePair<ushort, ushort> resources;

        public void Read(ArraySegment<byte> segment)
        {
            throw new NotImplementedException();
        }

        public ArraySegment<byte> Write()
        {
            throw new NotImplementedException();
        }
    }
}
