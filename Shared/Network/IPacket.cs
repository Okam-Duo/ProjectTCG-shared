using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Network
{
    public interface IPacket
    {
        public void Read(in ArraySegment<byte> segment);
        public ArraySegment<byte> Write();
    }
}
