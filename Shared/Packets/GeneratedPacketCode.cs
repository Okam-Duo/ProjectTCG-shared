#pragma warning disable 8618    //생성자에서 null을 허용하지 않는 필드를 초기화하지 않음 경고 비활성화

using Shared.Network;
using Shared.Contents;
using System.Text;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

//해당 파일은 PacketGenerator.Program에 의해 자동 생성되었습니다.

namespace Shared.Packets
{
    #region 패킷ID enum
    public enum PacketID
    {
        Null = 0,
        C_ConnectServerReq = 1,
        S_ConnectServerRes = 2,
        C_CheckIdAvailableReq = 3,
        S_CheckIdAvailableRes = 4,
        C_SignInReq = 5,
        S_SignInRes = 6,
        C_LoginReq = 7,
        S_LoginRes = 8,
        C_LogoutReq = 9,
        C_CurrencyInfoReq = 10,
        S_CurrencyInfoRes = 11,
        C_TryMatchingReq = 12,
        S_TryMatchingRes = 13,
        S_MatchingSuccess = 14,
        S_GameRoomStart = 15,
        C_SurrenderReq = 16,
        S_GameResult = 17,
        C_UseCardReq = 18,
        S_UseCardRes = 19,
        C_TurnEndReq = 20,
        S_TurnEnd = 21,
        S_IngameActionChain = 22,
        C_ShopInfoReq = 23,
        S_ShopInfoRes = 24,
        C_BuyShopItemReq = 25,
        S_BuyShopItemRes = 26,
        C_DeckInfoReq = 27,
        S_DeckInfoRes = 28,
        C_DeckEditReq = 29,
        S_DeckEditRes = 30,
    }
    #endregion

    #region PacketFactory : ID기반 패킷 생성기
    public static class PacketFactory
    {
        private static Dictionary<PacketID, Func<ArraySegment<byte>, IPacket>> _packetFactory =
        new() {
        {PacketID.Null,ErrorHandle},
        {PacketID.C_ConnectServerReq,Read<C_ConnectServerReq>},
        {PacketID.S_ConnectServerRes,Read<S_ConnectServerRes>},
        {PacketID.C_CheckIdAvailableReq,Read<C_CheckIdAvailableReq>},
        {PacketID.S_CheckIdAvailableRes,Read<S_CheckIdAvailableRes>},
        {PacketID.C_SignInReq,Read<C_SignInReq>},
        {PacketID.S_SignInRes,Read<S_SignInRes>},
        {PacketID.C_LoginReq,Read<C_LoginReq>},
        {PacketID.S_LoginRes,Read<S_LoginRes>},
        {PacketID.C_LogoutReq,Read<C_LogoutReq>},
        {PacketID.C_CurrencyInfoReq,Read<C_CurrencyInfoReq>},
        {PacketID.S_CurrencyInfoRes,Read<S_CurrencyInfoRes>},
        {PacketID.C_TryMatchingReq,Read<C_TryMatchingReq>},
        {PacketID.S_TryMatchingRes,Read<S_TryMatchingRes>},
        {PacketID.S_MatchingSuccess,Read<S_MatchingSuccess>},
        {PacketID.S_GameRoomStart,Read<S_GameRoomStart>},
        {PacketID.C_SurrenderReq,Read<C_SurrenderReq>},
        {PacketID.S_GameResult,Read<S_GameResult>},
        {PacketID.C_UseCardReq,Read<C_UseCardReq>},
        {PacketID.S_UseCardRes,Read<S_UseCardRes>},
        {PacketID.C_TurnEndReq,Read<C_TurnEndReq>},
        {PacketID.S_TurnEnd,Read<S_TurnEnd>},
        {PacketID.S_IngameActionChain,Read<S_IngameActionChain>},
        {PacketID.C_ShopInfoReq,Read<C_ShopInfoReq>},
        {PacketID.S_ShopInfoRes,Read<S_ShopInfoRes>},
        {PacketID.C_BuyShopItemReq,Read<C_BuyShopItemReq>},
        {PacketID.S_BuyShopItemRes,Read<S_BuyShopItemRes>},
        {PacketID.C_DeckInfoReq,Read<C_DeckInfoReq>},
        {PacketID.S_DeckInfoRes,Read<S_DeckInfoRes>},
        {PacketID.C_DeckEditReq,Read<C_DeckEditReq>},
        {PacketID.S_DeckEditRes,Read<S_DeckEditRes>},
        };

        //외부 공개용 인터페이스
        public static IPacket GeneratePacket(PacketID packetID, ArraySegment<byte> buffer)
        {
            if (_packetFactory.Count < (int)packetID)
            {
                Logger.Log($"할당되지 않은 PacketID를 가진 패킷이 수신되었습니다. \nPacketID : {(int)packetID}");
            }

            return _packetFactory[packetID](buffer);
        }

        //패킷 생성용 private 함수
        private static IPacket Read<T>(ArraySegment<byte> buffer) where T : IPacket, new()
        {
            T packet = new T();
            packet.Read(buffer);
            return packet;
        }

        private static IPacket ErrorHandle(ArraySegment<byte> buffer)
        {
            Logger.Log("할당되지 않은 PacketID를 가진 패킷이 수신되었습니다. \nPacketID : 0");
            return null;
        }
    }
    #endregion


    #region 1. C_ConnectServerReq
    public struct C_ConnectServerReq : IPacket
    {


        public PacketID PacketID => PacketID.C_ConnectServerReq;



        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_ConnectServerReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                Logger.Log("C_ConnectServerReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 2. S_ConnectServerRes
    public struct S_ConnectServerRes : IPacket
    {


        public PacketID PacketID => PacketID.S_ConnectServerRes;



        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_ConnectServerRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                Logger.Log("S_ConnectServerRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 3. C_CheckIdAvailableReq
    public struct C_CheckIdAvailableReq : IPacket
    {
        public string id;

        public PacketID PacketID => PacketID.C_CheckIdAvailableReq;

        public C_CheckIdAvailableReq(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_CheckIdAvailableReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //string id
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            id = Encoding.UTF8.GetString(segment.Array, segment.Offset + c, len);
            c += len;

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


            len = (ushort)Encoding.UTF8.GetBytes(id, 0, id.Length, segment.Array, segment.Offset + c + sizeof(ushort));
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);
            c += sizeof(ushort);
            c += len;



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_CheckIdAvailableReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 4. S_CheckIdAvailableRes
    public struct S_CheckIdAvailableRes : IPacket
    {
        public bool isIdAvailable;

        public PacketID PacketID => PacketID.S_CheckIdAvailableRes;

        public S_CheckIdAvailableRes(bool isIdAvailable)
        {
            this.isIdAvailable = isIdAvailable;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_CheckIdAvailableRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //bool isIdAvailable
            isIdAvailable = BitConverter.ToBoolean(s.Slice(c, segment.Count - c));
            c += sizeof(bool);

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


            //bool isIdAvailable
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), isIdAvailable);
            c += sizeof(bool);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_CheckIdAvailableRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 5. C_SignInReq
    public struct C_SignInReq : IPacket
    {
        public string id;
        public int passward;
        public string nickName;

        public PacketID PacketID => PacketID.C_SignInReq;

        public C_SignInReq(string id, int passward, string nickName)
        {
            this.id = id;
            this.passward = passward;
            this.nickName = nickName;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_SignInReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //string id
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            id = Encoding.UTF8.GetString(segment.Array, segment.Offset + c, len);
            c += len;

            //int passward
            passward = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //string nickName
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            nickName = Encoding.UTF8.GetString(segment.Array, segment.Offset + c, len);
            c += len;

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


            len = (ushort)Encoding.UTF8.GetBytes(id, 0, id.Length, segment.Array, segment.Offset + c + sizeof(ushort));
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);
            c += sizeof(ushort);
            c += len;

            //int passward
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), passward);
            c += sizeof(int);

            len = (ushort)Encoding.UTF8.GetBytes(nickName, 0, nickName.Length, segment.Array, segment.Offset + c + sizeof(ushort));
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);
            c += sizeof(ushort);
            c += len;



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_SignInReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 6. S_SignInRes
    public struct S_SignInRes : IPacket
    {
        public bool isSuccess;

        public PacketID PacketID => PacketID.S_SignInRes;

        public S_SignInRes(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_SignInRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //bool isSuccess
            isSuccess = BitConverter.ToBoolean(s.Slice(c, segment.Count - c));
            c += sizeof(bool);

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



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_SignInRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 7. C_LoginReq
    public struct C_LoginReq : IPacket
    {
        public string id;
        public int passward;

        public PacketID PacketID => PacketID.C_LoginReq;

        public C_LoginReq(string id, int passward)
        {
            this.id = id;
            this.passward = passward;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_LoginReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //string id
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            id = Encoding.UTF8.GetString(segment.Array, segment.Offset + c, len);
            c += len;

            //int passward
            passward = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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


            len = (ushort)Encoding.UTF8.GetBytes(id, 0, id.Length, segment.Array, segment.Offset + c + sizeof(ushort));
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);
            c += sizeof(ushort);
            c += len;

            //int passward
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), passward);
            c += sizeof(int);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_LoginReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 8. S_LoginRes
    public struct S_LoginRes : IPacket
    {
        public bool isSuccess;
        public string nickName;
        public int uid;

        public PacketID PacketID => PacketID.S_LoginRes;

        public S_LoginRes(bool isSuccess, string nickName, int uid)
        {
            this.isSuccess = isSuccess;
            this.nickName = nickName;
            this.uid = uid;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_LoginRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //bool isSuccess
            isSuccess = BitConverter.ToBoolean(s.Slice(c, segment.Count - c));
            c += sizeof(bool);

            //string nickName
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            nickName = Encoding.UTF8.GetString(segment.Array, segment.Offset + c, len);
            c += len;

            //int uid
            uid = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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

            len = (ushort)Encoding.UTF8.GetBytes(nickName, 0, nickName.Length, segment.Array, segment.Offset + c + sizeof(ushort));
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);
            c += sizeof(ushort);
            c += len;

            //int uid
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), uid);
            c += sizeof(int);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_LoginRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 9. C_LogoutReq
    public struct C_LogoutReq : IPacket
    {


        public PacketID PacketID => PacketID.C_LogoutReq;



        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_LogoutReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                Logger.Log("C_LogoutReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 10. C_CurrencyInfoReq
    public struct C_CurrencyInfoReq : IPacket
    {


        public PacketID PacketID => PacketID.C_CurrencyInfoReq;



        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_CurrencyInfoReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                Logger.Log("C_CurrencyInfoReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 11. S_CurrencyInfoRes
    public struct S_CurrencyInfoRes : IPacket
    {
        public int gold;

        public PacketID PacketID => PacketID.S_CurrencyInfoRes;

        public S_CurrencyInfoRes(int gold)
        {
            this.gold = gold;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_CurrencyInfoRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int gold
            gold = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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


            //int gold
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), gold);
            c += sizeof(int);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_CurrencyInfoRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 12. C_TryMatchingReq
    public struct C_TryMatchingReq : IPacket
    {
        public int usedDeckId;

        public PacketID PacketID => PacketID.C_TryMatchingReq;

        public C_TryMatchingReq(int usedDeckId)
        {
            this.usedDeckId = usedDeckId;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_TryMatchingReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int usedDeckId
            usedDeckId = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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


            //int usedDeckId
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), usedDeckId);
            c += sizeof(int);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_TryMatchingReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 13. S_TryMatchingRes
    public struct S_TryMatchingRes : IPacket
    {
        public bool isMatchingStarted;

        public PacketID PacketID => PacketID.S_TryMatchingRes;

        public S_TryMatchingRes(bool isMatchingStarted)
        {
            this.isMatchingStarted = isMatchingStarted;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_TryMatchingRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //bool isMatchingStarted
            isMatchingStarted = BitConverter.ToBoolean(s.Slice(c, segment.Count - c));
            c += sizeof(bool);

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


            //bool isMatchingStarted
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), isMatchingStarted);
            c += sizeof(bool);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_TryMatchingRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 14. S_MatchingSuccess
    public struct S_MatchingSuccess : IPacket
    {


        public PacketID PacketID => PacketID.S_MatchingSuccess;



        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_MatchingSuccess));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                Logger.Log("S_MatchingSuccess.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 15. S_GameRoomStart
    public struct S_GameRoomStart : IPacket
    {
        public int[] handsCardId;
        public int firstPlayerOid;
        public Shared.Contents.IngamePlayerData[] playerDatas;

        public PacketID PacketID => PacketID.S_GameRoomStart;

        public S_GameRoomStart(int[] handsCardId, int firstPlayerOid, Shared.Contents.IngamePlayerData[] playerDatas)
        {
            this.handsCardId = handsCardId;
            this.firstPlayerOid = firstPlayerOid;
            this.playerDatas = playerDatas;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_GameRoomStart));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                //int handsCardId[i]
                handsCardId[i] = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
                c += sizeof(int);
            }

            //int firstPlayerOid
            firstPlayerOid = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //struct playerDatas[i]
                playerDatas[i].Read(segment, ref c);
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


            //int[] handsCardId
            len = (ushort)handsCardId.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                //int handsCardId[i]
                success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), handsCardId[i]);
                c += sizeof(int);
            }

            //int firstPlayerOid
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), firstPlayerOid);
            c += sizeof(int);

            //Shared.Contents.IngamePlayerData[] playerDatas
            len = (ushort)playerDatas.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                playerDatas[i].Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);
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
                Logger.Log("S_GameRoomStart.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 16. C_SurrenderReq
    public struct C_SurrenderReq : IPacket
    {


        public PacketID PacketID => PacketID.C_SurrenderReq;



        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_SurrenderReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                Logger.Log("C_SurrenderReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 17. S_GameResult
    public struct S_GameResult : IPacket
    {
        public int winnerOid;
        public bool isDraw;
        public GameEndReason reason;
        public int rewardGold;

        public PacketID PacketID => PacketID.S_GameResult;

        public S_GameResult(int winnerOid, bool isDraw, GameEndReason reason, int rewardGold)
        {
            this.winnerOid = winnerOid;
            this.isDraw = isDraw;
            this.reason = reason;
            this.rewardGold = rewardGold;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_GameResult));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int winnerOid
            winnerOid = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //bool isDraw
            isDraw = BitConverter.ToBoolean(s.Slice(c, segment.Count - c));
            c += sizeof(bool);

            //GameEndReason reason
            reason = (GameEndReason)BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);

            //int rewardGold
            rewardGold = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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


            //int winnerOid
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), winnerOid);
            c += sizeof(int);

            //bool isDraw
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), isDraw);
            c += sizeof(bool);

            //GameEndReason reason
            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)reason);
            c += sizeof(ushort);

            //int rewardGold
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), rewardGold);
            c += sizeof(int);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_GameResult.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 18. C_UseCardReq
    public struct C_UseCardReq : IPacket
    {
        public int usedCardOid;

        public PacketID PacketID => PacketID.C_UseCardReq;

        public C_UseCardReq(int usedCardOid)
        {
            this.usedCardOid = usedCardOid;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_UseCardReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int usedCardOid
            usedCardOid = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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


            //int usedCardOid
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), usedCardOid);
            c += sizeof(int);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_UseCardReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 19. S_UseCardRes
    public struct S_UseCardRes : IPacket
    {
        public bool isSuccess;

        public PacketID PacketID => PacketID.S_UseCardRes;

        public S_UseCardRes(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_UseCardRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //bool isSuccess
            isSuccess = BitConverter.ToBoolean(s.Slice(c, segment.Count - c));
            c += sizeof(bool);

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



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_UseCardRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 20. C_TurnEndReq
    public struct C_TurnEndReq : IPacket
    {


        public PacketID PacketID => PacketID.C_TurnEndReq;



        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_TurnEndReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                Logger.Log("C_TurnEndReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 21. S_TurnEnd
    public struct S_TurnEnd : IPacket
    {
        public int endedPlayerOid;

        public PacketID PacketID => PacketID.S_TurnEnd;

        public S_TurnEnd(int endedPlayerOid)
        {
            this.endedPlayerOid = endedPlayerOid;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_TurnEnd));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int endedPlayerOid
            endedPlayerOid = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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


            //int endedPlayerOid
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), endedPlayerOid);
            c += sizeof(int);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_TurnEnd.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 22. S_IngameActionChain
    public struct S_IngameActionChain : IPacket
    {
        public IngameAction[] actionDatas;

        public PacketID PacketID => PacketID.S_IngameActionChain;

        public S_IngameActionChain(IngameAction[] actionDatas)
        {
            this.actionDatas = actionDatas;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_IngameActionChain));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                //struct actionDatas[i]
                actionDatas[i].Read(segment, ref c);
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


            //IngameAction[] actionDatas
            len = (ushort)actionDatas.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                actionDatas[i].Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);
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
                Logger.Log("S_IngameActionChain.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 23. C_ShopInfoReq
    public struct C_ShopInfoReq : IPacket
    {


        public PacketID PacketID => PacketID.C_ShopInfoReq;



        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_ShopInfoReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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

    #region 24. S_ShopInfoRes
    public struct S_ShopInfoRes : IPacket
    {
        public Shared.Contents.ShopItemInfo[] shopItemsInfo;

        public PacketID PacketID => PacketID.S_ShopInfoRes;

        public S_ShopInfoRes(Shared.Contents.ShopItemInfo[] shopItemsInfo)
        {
            this.shopItemsInfo = shopItemsInfo;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_ShopInfoRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                //struct shopItemsInfo[i]
                shopItemsInfo[i].Read(segment, ref c);
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


            //Shared.Contents.ShopItemInfo[] shopItemsInfo
            len = (ushort)shopItemsInfo.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                shopItemsInfo[i].Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);
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

    #region 25. C_BuyShopItemReq
    public struct C_BuyShopItemReq : IPacket
    {
        public int boughtShopItemId;

        public PacketID PacketID => PacketID.C_BuyShopItemReq;

        public C_BuyShopItemReq(int boughtShopItemId)
        {
            this.boughtShopItemId = boughtShopItemId;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_BuyShopItemReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int boughtShopItemId
            boughtShopItemId = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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


            //int boughtShopItemId
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), boughtShopItemId);
            c += sizeof(int);



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

    #region 26. S_BuyShopItemRes
    public struct S_BuyShopItemRes : IPacket
    {
        public bool isSuccess;
        public Shared.Contents.Item[] rewards;

        public PacketID PacketID => PacketID.S_BuyShopItemRes;

        public S_BuyShopItemRes(bool isSuccess, Shared.Contents.Item[] rewards)
        {
            this.isSuccess = isSuccess;
            this.rewards = rewards;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_BuyShopItemRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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

            //Shared.Contents.Item[] rewards
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

    #region 27. C_DeckInfoReq
    public struct C_DeckInfoReq : IPacket
    {
        public int deckIndex;

        public PacketID PacketID => PacketID.C_DeckInfoReq;

        public C_DeckInfoReq(int deckIndex)
        {
            this.deckIndex = deckIndex;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_DeckInfoReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int deckIndex
            deckIndex = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

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


            //int deckIndex
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), deckIndex);
            c += sizeof(int);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("C_DeckInfoReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 28. S_DeckInfoRes
    public struct S_DeckInfoRes : IPacket
    {
        public int[] heroesId;
        public int[] cardsId;

        public PacketID PacketID => PacketID.S_DeckInfoRes;

        public S_DeckInfoRes(int[] heroesId, int[] cardsId)
        {
            this.heroesId = heroesId;
            this.cardsId = cardsId;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_DeckInfoRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

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
                //int heroesId[i]
                heroesId[i] = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
                c += sizeof(int);
            }

            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //int cardsId[i]
                cardsId[i] = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
                c += sizeof(int);
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


            //int[] heroesId
            len = (ushort)heroesId.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                //int heroesId[i]
                success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), heroesId[i]);
                c += sizeof(int);
            }

            //int[] cardsId
            len = (ushort)cardsId.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                //int cardsId[i]
                success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), cardsId[i]);
                c += sizeof(int);
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
                Logger.Log("S_DeckInfoRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 29. C_DeckEditReq
    public struct C_DeckEditReq : IPacket
    {
        public int deckIndex;
        public int[] heroesId;
        public int[] cardsId;

        public PacketID PacketID => PacketID.C_DeckEditReq;

        public C_DeckEditReq(int deckIndex, int[] heroesId, int[] cardsId)
        {
            this.deckIndex = deckIndex;
            this.heroesId = heroesId;
            this.cardsId = cardsId;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(C_DeckEditReq));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int deckIndex
            deckIndex = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //int heroesId[i]
                heroesId[i] = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
                c += sizeof(int);
            }

            //Array
            len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
            for (int i = 0; i < len; i++)
            {
                //int cardsId[i]
                cardsId[i] = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
                c += sizeof(int);
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


            //int deckIndex
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), deckIndex);
            c += sizeof(int);

            //int[] heroesId
            len = (ushort)heroesId.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                //int heroesId[i]
                success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), heroesId[i]);
                c += sizeof(int);
            }

            //int[] cardsId
            len = (ushort)cardsId.Length;//*
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
            c += sizeof(ushort);//*
            for (int i = 0; i < len; i++)
            {
                //int cardsId[i]
                success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), cardsId[i]);
                c += sizeof(int);
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
                Logger.Log("C_DeckEditReq.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion

    #region 30. S_DeckEditRes
    public struct S_DeckEditRes : IPacket
    {
        public int deckIndex;
        public bool isSuccess;

        public PacketID PacketID => PacketID.S_DeckEditRes;

        public S_DeckEditRes(int deckIndex, bool isSuccess)
        {
            this.deckIndex = deckIndex;
            this.isSuccess = isSuccess;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(S_DeckEditRes));
            StringBuilder stringBuilder = new();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);

            xml.Serialize(xmlWriter, this);
            string text = stringBuilder.ToString();
            return text;
        }

        public void Read(in ArraySegment<byte> segment)
        {
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //int deckIndex
            deckIndex = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);

            //bool isSuccess
            isSuccess = BitConverter.ToBoolean(s.Slice(c, segment.Count - c));
            c += sizeof(bool);

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


            //int deckIndex
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), deckIndex);
            c += sizeof(int);

            //bool isSuccess
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), isSuccess);
            c += sizeof(bool);



            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return result;
            }
            else
            {
                Logger.Log("S_DeckEditRes.Write() : Failed to write serialized packet data");
                return null;
            }
        }
    }
    #endregion


    #region 패킷 핸들러 인터페이스
    public interface IPacketHandler
    {
        public void RunPakcetHandle(Session session, PacketID packetID, IPacket packet)
        {
            switch (packetID)
            {
                case PacketID.C_ConnectServerReq:
                    C_ConnectServerReq_Handle(session, (C_ConnectServerReq)packet);
                    break;
                case PacketID.S_ConnectServerRes:
                    S_ConnectServerRes_Handle(session, (S_ConnectServerRes)packet);
                    break;
                case PacketID.C_CheckIdAvailableReq:
                    C_CheckIdAvailableReq_Handle(session, (C_CheckIdAvailableReq)packet);
                    break;
                case PacketID.S_CheckIdAvailableRes:
                    S_CheckIdAvailableRes_Handle(session, (S_CheckIdAvailableRes)packet);
                    break;
                case PacketID.C_SignInReq:
                    C_SignInReq_Handle(session, (C_SignInReq)packet);
                    break;
                case PacketID.S_SignInRes:
                    S_SignInRes_Handle(session, (S_SignInRes)packet);
                    break;
                case PacketID.C_LoginReq:
                    C_LoginReq_Handle(session, (C_LoginReq)packet);
                    break;
                case PacketID.S_LoginRes:
                    S_LoginRes_Handle(session, (S_LoginRes)packet);
                    break;
                case PacketID.C_LogoutReq:
                    C_LogoutReq_Handle(session, (C_LogoutReq)packet);
                    break;
                case PacketID.C_CurrencyInfoReq:
                    C_CurrencyInfoReq_Handle(session, (C_CurrencyInfoReq)packet);
                    break;
                case PacketID.S_CurrencyInfoRes:
                    S_CurrencyInfoRes_Handle(session, (S_CurrencyInfoRes)packet);
                    break;
                case PacketID.C_TryMatchingReq:
                    C_TryMatchingReq_Handle(session, (C_TryMatchingReq)packet);
                    break;
                case PacketID.S_TryMatchingRes:
                    S_TryMatchingRes_Handle(session, (S_TryMatchingRes)packet);
                    break;
                case PacketID.S_MatchingSuccess:
                    S_MatchingSuccess_Handle(session, (S_MatchingSuccess)packet);
                    break;
                case PacketID.S_GameRoomStart:
                    S_GameRoomStart_Handle(session, (S_GameRoomStart)packet);
                    break;
                case PacketID.C_SurrenderReq:
                    C_SurrenderReq_Handle(session, (C_SurrenderReq)packet);
                    break;
                case PacketID.S_GameResult:
                    S_GameResult_Handle(session, (S_GameResult)packet);
                    break;
                case PacketID.C_UseCardReq:
                    C_UseCardReq_Handle(session, (C_UseCardReq)packet);
                    break;
                case PacketID.S_UseCardRes:
                    S_UseCardRes_Handle(session, (S_UseCardRes)packet);
                    break;
                case PacketID.C_TurnEndReq:
                    C_TurnEndReq_Handle(session, (C_TurnEndReq)packet);
                    break;
                case PacketID.S_TurnEnd:
                    S_TurnEnd_Handle(session, (S_TurnEnd)packet);
                    break;
                case PacketID.S_IngameActionChain:
                    S_IngameActionChain_Handle(session, (S_IngameActionChain)packet);
                    break;
                case PacketID.C_ShopInfoReq:
                    C_ShopInfoReq_Handle(session, (C_ShopInfoReq)packet);
                    break;
                case PacketID.S_ShopInfoRes:
                    S_ShopInfoRes_Handle(session, (S_ShopInfoRes)packet);
                    break;
                case PacketID.C_BuyShopItemReq:
                    C_BuyShopItemReq_Handle(session, (C_BuyShopItemReq)packet);
                    break;
                case PacketID.S_BuyShopItemRes:
                    S_BuyShopItemRes_Handle(session, (S_BuyShopItemRes)packet);
                    break;
                case PacketID.C_DeckInfoReq:
                    C_DeckInfoReq_Handle(session, (C_DeckInfoReq)packet);
                    break;
                case PacketID.S_DeckInfoRes:
                    S_DeckInfoRes_Handle(session, (S_DeckInfoRes)packet);
                    break;
                case PacketID.C_DeckEditReq:
                    C_DeckEditReq_Handle(session, (C_DeckEditReq)packet);
                    break;
                case PacketID.S_DeckEditRes:
                    S_DeckEditRes_Handle(session, (S_DeckEditRes)packet);
                    break;

            }
        }

        void C_ConnectServerReq_Handle(Session session, C_ConnectServerReq packet);
        void S_ConnectServerRes_Handle(Session session, S_ConnectServerRes packet);
        void C_CheckIdAvailableReq_Handle(Session session, C_CheckIdAvailableReq packet);
        void S_CheckIdAvailableRes_Handle(Session session, S_CheckIdAvailableRes packet);
        void C_SignInReq_Handle(Session session, C_SignInReq packet);
        void S_SignInRes_Handle(Session session, S_SignInRes packet);
        void C_LoginReq_Handle(Session session, C_LoginReq packet);
        void S_LoginRes_Handle(Session session, S_LoginRes packet);
        void C_LogoutReq_Handle(Session session, C_LogoutReq packet);
        void C_CurrencyInfoReq_Handle(Session session, C_CurrencyInfoReq packet);
        void S_CurrencyInfoRes_Handle(Session session, S_CurrencyInfoRes packet);
        void C_TryMatchingReq_Handle(Session session, C_TryMatchingReq packet);
        void S_TryMatchingRes_Handle(Session session, S_TryMatchingRes packet);
        void S_MatchingSuccess_Handle(Session session, S_MatchingSuccess packet);
        void S_GameRoomStart_Handle(Session session, S_GameRoomStart packet);
        void C_SurrenderReq_Handle(Session session, C_SurrenderReq packet);
        void S_GameResult_Handle(Session session, S_GameResult packet);
        void C_UseCardReq_Handle(Session session, C_UseCardReq packet);
        void S_UseCardRes_Handle(Session session, S_UseCardRes packet);
        void C_TurnEndReq_Handle(Session session, C_TurnEndReq packet);
        void S_TurnEnd_Handle(Session session, S_TurnEnd packet);
        void S_IngameActionChain_Handle(Session session, S_IngameActionChain packet);
        void C_ShopInfoReq_Handle(Session session, C_ShopInfoReq packet);
        void S_ShopInfoRes_Handle(Session session, S_ShopInfoRes packet);
        void C_BuyShopItemReq_Handle(Session session, C_BuyShopItemReq packet);
        void S_BuyShopItemRes_Handle(Session session, S_BuyShopItemRes packet);
        void C_DeckInfoReq_Handle(Session session, C_DeckInfoReq packet);
        void S_DeckInfoRes_Handle(Session session, S_DeckInfoRes packet);
        void C_DeckEditReq_Handle(Session session, C_DeckEditReq packet);
        void S_DeckEditRes_Handle(Session session, S_DeckEditRes packet);

    }
    #endregion 

}