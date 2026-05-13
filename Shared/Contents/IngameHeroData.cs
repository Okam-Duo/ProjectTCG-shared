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
    public struct IngameHeroData : ISerializableData
    {
        public int id;
        public int oid;
        public int currentHp;

        public IngameHeroData(int id, int oid, int currentHp)
        {
            this.id = id;
            this.oid = oid;
            this.currentHp = currentHp;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(IngameHeroData));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment, ref int c)
        {
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int id
            id = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //int oid
            oid = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //int currentHp
            currentHp = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

        }

        public bool Write(ArraySegment<byte> segment, ref int c)
        {
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;


            //int id
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), id);
            c += sizeof(int);

            //int oid
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), oid);
            c += sizeof(int);

            //int currentHp
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), currentHp);
            c += sizeof(int);

            return success;
        }
    }
}
