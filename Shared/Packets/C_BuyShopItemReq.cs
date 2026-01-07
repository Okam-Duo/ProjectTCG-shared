using Shared.Network;
using System.Text;

namespace Shared.Packets
{
    public struct C_BuyShopItemReq : Network.IPacket//
    {
        public ushort itemIndex;//*
        public string name;//*
        public int[] ints;//*
        public TestData data;
        public TestData[] datas;

        public PacketID PacketID => PacketID.C_BuyShopItemReq;//

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            itemIndex = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);

            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            name = Encoding.UTF8.GetString(segment.Array, segment.Offset + c, len);
            c += len;

            data.Read(segment, ref c);

            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                datas[i].Read(segment, ref c);
            }
        }

        public ArraySegment<byte> Write()
        {
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            int c = 0;
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);

            //패킷 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);

            //패킷ID 삽입
            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)PacketID);
            c += sizeof(ushort);


            //ushort itemIndex
            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), itemIndex);//*
            c += sizeof(ushort);//*

            //string name
            len = (ushort)Encoding.UTF8.GetBytes(name, 0, name.Length, segment.Array, segment.Offset + c + sizeof(ushort));//*
            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            c += len;//*

            //int[] ints
            len = (ushort)ints.Length;//*
            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), ints[i]);//*
                c += sizeof(int);
            }

            //TestData data
            data.Write(s.Slice(c, s.Length - c), ref c);//*

            //TestData[] datas
            len = (ushort)datas.Length;//*
            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                datas[i].Write(s.Slice(c, s.Length - c), ref c);//*
            }


            //패킷 크기 삽입
            BitConverter.TryWriteBytes(s, (ushort)c);//

            return SendBufferHelper.Close(c);
        }
    }

    public struct TestData : ISerializableData
    {
        public void Read(ReadOnlySpan<byte> segment, ref int count)
        {
            throw new NotImplementedException();
        }

        public bool Write(Span<byte> segment, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}
