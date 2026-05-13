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
    public struct IngamePlayerData : ISerializableData
    {
        public string nickName;
        public int oid;
        public IngameHeroData[] heroes;
        public int[] handCardsOid;

        public IngamePlayerData(string nickName, int oid, IngameHeroData[] heroes, int[] handCardsOid)
        {
            this.nickName = nickName;
            this.oid = oid;
            this.heroes = heroes;
            this.handCardsOid = handCardsOid;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(IngamePlayerData));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment, ref int c)
        {
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //string nickName
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            nickName = Encoding.UTF8.GetString(segment.Array, segment.Offset + c, len);
            c += len;

            //int oid
            oid = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //struct heroes[i]
                heroes[i].Read(segment, ref c);
            }

            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //int handCardsOid[i]
                handCardsOid[i] = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
                c += sizeof(int);
            }

        }

        public bool Write(ArraySegment<byte> segment, ref int c)
        {
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;


            len = (ushort)Encoding.UTF8.GetBytes(nickName, 0, nickName.Length, segment.Array, segment.Offset + c + sizeof(ushort));
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);
            c += sizeof(ushort);
            c += len;

            //int oid
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), oid);
            c += sizeof(int);

            //IngameHeroData[] heroes
            len = (ushort)heroes.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                heroes[i].Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);
            }

            //int[] handCardsOid
            len = (ushort)handCardsOid.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                //int handCardsOid[i]
                success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), handCardsOid[i]);
                c += sizeof(int);
            }

            return success;
        }
    }
}
