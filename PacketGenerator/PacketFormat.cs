namespace PacketGenerator
{
    internal class PacketFormat
    {
        //{0} 패킷ID들
        //{1} 패킷 선언들
        public static string fileFormat =
@"using Shared.Network;
using Shared.Contents;
using System.Text;
using System;

//해당 파일은 PacketGenerator.Program에 의해 자동 생성되었습니다.

namespace Shared.Packets
{{
    public enum PacketID
    {{
        Null = 0,{0}
    }}

{1}

}}";

        //{0} 패킷 이름
        //{1} 멤버 변수 선언
        //{2} 멤버 변수 Read
        //{3} 멤버 변수 Write
        //{4} 패킷 ID
        public static string packetFormat =
@"
#region {4}. {0}
    public struct {0} : IPacket
    {{
        {1}

        public PacketID PacketID => PacketID.{0};


        public void Read(in ArraySegment<byte> segment)
        {{
            int c = 0;
            int len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            {2}
        }}

        public ArraySegment<byte> Write()
        {{
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


            {3}


            //패킷 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {{
                return result;
            }}
            else
            {{
                Logger.Log(""{0}.Write() : Failed to write serialized packet data"");
                return null;
            }}
        }}
    }}
#endregion
";

        //{0} 멤버 변수 형식
        //{1} 멤버 변수 이름
        public static string memberFormat = "public {0} {1};";

        //{0} 멤버 변수 형식
        //{1} 멤버 변수 이름
        public static string memberArrayFormat = "public {0}[] {1};";

        //{0} 멤버 변수 이름
        //{1} To타입 함수
        //{2} 멤버 변수 형식
        public static string readFormat =
@"//{2} {0}
{0} = BitConverter.{1}(s.Slice(c, segment.Count - c));
c += sizeof({2});";

        //{0} 멤버 변수 이름
        public static string readStringFormat =
@"//string {0}
len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
c += sizeof(ushort);
{0} = Encoding.UTF8.GetString(segment.Array, segment.Offset + c, len);
c += len;";

        //{0} 멤버 변수 이름
        public static string readStructFormat = "//struct {0}\n{0}.Read(segment, ref c);";

        //{0} readFormat, 멤버 변수 이름을 "배열이름[i]"로 정해야함
        public static string readArrayFormat =
@"//Array
len = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
c += sizeof(ushort);
for (int i = 0; i < len; i++)
{{
    {0}
}}";

        //{0} 멤버 변수 이름
        public static string readByteFormat =
@"//byte {0}
{0} = segment.Array[segment.Offset + c];
c += sizeof(byte);";

        //{0} 멤버 변수 이름
        //{1} 멤버 변수 형식
        public static string readEnumFormat =
@"//{1} {0}
{0} = ({1})BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
c += sizeof(ushort);";

        //{0} 멤버 변수 이름
        //{1} 멤버 변수 형식
        public static string writeFormat =
@"//{1} {0}
success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), {0});
c += sizeof({1});";

        //{0} 멤버 변수 이름
        public static string writeStringFormat =
@"len = (ushort)Encoding.UTF8.GetBytes({0}, 0, {0}.Length, segment.Array, segment.Offset + c + sizeof(ushort));
success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);
c += sizeof(ushort);
c += len;";

        //{0} 멤버 변수 이름
        public static string writeStructFormat = "{0}.Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);";

        //{0} 멤버 배열 이름
        //{1} 배열이 다루는 형식
        //{2} writeFormat, 멤버 변수 이름을 "배열이름[i]"로 정해야함
        public static string writeArrayFormat =
@"//{1}[] {0}
len = (ushort){0}.Length;//*
success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), len);//*
c += sizeof(ushort);//*
for (int i = 0; i < len; i++)
{{
    {2}
}}";

        //{0} 멤버 변수 이름
        public static string writeByteFormat =
@"//byte {0}
segment.Array[segment.Offset + c] = {0};
c += sizeof(byte);";

        //{0} 멤버 변수 이름
        //{1} enum 타입 이름
        public static string writeEnumFormat =
@"//{1} {0}
BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort){0});
c += sizeof(ushort);";
    }
}
