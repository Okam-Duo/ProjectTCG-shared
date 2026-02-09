using Shared.Network;
using Shared.Packets;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Shared.Contents
{
    public struct ShopItemInfo : ISerializableData
    {
        public int packId;
        public int cost;

        public ShopItemInfo(int packId, int cost)
        {
            this.packId = packId;
            this.cost = cost;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(ShopItemInfo));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment,ref int c)
        {
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int packId
            packId = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //int cost
            cost = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

        }

        public bool Write(ArraySegment<byte> segment, ref int c)
        {
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;

            //전체 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);


            //int packId
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), packId);
            c += sizeof(int);

            //int cost
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), cost);
            c += sizeof(int);



            //전체 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);

            if (success)
            {
                return true;
            }
            else
            {
                Logger.Log("ShopItemInfo.Write() : Failed to write serialized data");
                return false;
            }
        }
    }
}
