using Shared.Network;
using Shared.Contents;
using System.Text;

//해당 파일은 PacketGenerator.Program에 의해 자동 생성되었습니다.

namespace Shared.Packets
{
    public enum PacketID
    {
        Null = 0,
        C_ResourceInfoReq = 1,
        S_ResourceInfoRes = 2,
        C_ShopInfoReq = 3,
        S_ShopInfoRes = 4,
        C_BuyShopItemReq = 5,
        S_BuyShopItemRes = 6,
    }


    #region 1. C_ResourceInfoReq
    public struct C_ResourceInfoReq : IPacket
    {
        public ResourceType[] resourceType;

        public PacketID PacketID => PacketID.C_ResourceInfoReq;


        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //ResourceType resourceType[i]
                resourceType[i] = (ResourceType)BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
                c += sizeof(ushort);
            }

        }

        public ArraySegment<byte> Write()
        {
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            int c = 0;
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;

            //패킷 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);

            //패킷ID 삽입
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)PacketID);
            c += sizeof(ushort);


            //enum[] resourceType
            len = (ushort)resourceType.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                //ResourceType resourceType[i]
                BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)resourceType[i]);
                c += sizeof(ushort);
            }



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_ResourceInfoReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 2. S_ResourceInfoRes
    public struct S_ResourceInfoRes : IPacket
    {
        public ResourceInfo[] infos;

        public PacketID PacketID => PacketID.S_ResourceInfoRes;


        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //struct infos[i]
                infos[i].Read(segment, ref c);
            }

        }

        public ArraySegment<byte> Write()
        {
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            int c = 0;
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;

            //패킷 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);

            //패킷ID 삽입
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)PacketID);
            c += sizeof(ushort);


            //ResourceInfo[] infos
            len = (ushort)infos.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                infos[i].Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);
            }



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_ResourceInfoRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 3. C_ShopInfoReq
    public struct C_ShopInfoReq : IPacket
    {


        public PacketID PacketID => PacketID.C_ShopInfoReq;


        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);



        }

        public ArraySegment<byte> Write()
        {
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            int c = 0;
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;

            //패킷 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);

            //패킷ID 삽입
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)PacketID);
            c += sizeof(ushort);





            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_ShopInfoReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 4. S_ShopInfoRes
    public struct S_ShopInfoRes : IPacket
    {
        public Item[] items;

        public PacketID PacketID => PacketID.S_ShopInfoRes;


        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //struct items[i]
                items[i].Read(segment, ref c);
            }

        }

        public ArraySegment<byte> Write()
        {
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            int c = 0;
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;

            //패킷 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);

            //패킷ID 삽입
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)PacketID);
            c += sizeof(ushort);


            //Item[] items
            len = (ushort)items.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                items[i].Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);
            }



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_ShopInfoRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 5. C_BuyShopItemReq
    public struct C_BuyShopItemReq : IPacket
    {
        public ushort itemIndex;

        public PacketID PacketID => PacketID.C_BuyShopItemReq;


        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //ushort itemIndex
            itemIndex = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);

        }

        public ArraySegment<byte> Write()
        {
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            int c = 0;
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;

            //패킷 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);

            //패킷ID 삽입
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)PacketID);
            c += sizeof(ushort);


            //ushort itemIndex
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), itemIndex);
            c += sizeof(ushort);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_BuyShopItemReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 6. S_BuyShopItemRes
    public struct S_BuyShopItemRes : IPacket
    {
        public bool isSuccess;
        public Item[] rewards;

        public PacketID PacketID => PacketID.S_BuyShopItemRes;


        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //bool isSuccess
            isSuccess = BitConverter.ToBoolean(s.Slice(c, segment.Count - c));
            c += sizeof(bool);

            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //struct rewards[i]
                rewards[i].Read(segment, ref c);
            }

        }

        public ArraySegment<byte> Write()
        {
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            int c = 0;
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;

            //패킷 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);

            //패킷ID 삽입
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)PacketID);
            c += sizeof(ushort);


            //bool isSuccess
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), isSuccess);
            c += sizeof(bool);

            //Item[] rewards
            len = (ushort)rewards.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                rewards[i].Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);
            }



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_BuyShopItemRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion


}