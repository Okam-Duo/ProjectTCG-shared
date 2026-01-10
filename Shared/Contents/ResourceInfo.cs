using Shared.Network;
using Shared.Packets;
using System;

namespace Shared.Contents
{
    public struct ResourceInfo : ISerializableData
    {
        public ushort id;
        public ResourceType resourceType;
        public int count;

        public void Read(in ArraySegment<byte> segment, ref int count)
        {
            int c = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            //ushort id
            id = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);

            resourceType = (ResourceType)BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);

            //int count
            count = BitConverter.ToInt32(s.Slice(c, segment.Count - c));
            c += sizeof(int);
        }

        public bool Write(ArraySegment<byte> segment, ref int count)
        {
            int c = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;


            //ushort id
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), id);
            c += sizeof(ushort);

            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)resourceType);
            c += sizeof(ushort);

            //int count
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), count);
            c += sizeof(int);



            //패킷 크기 삽입
            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return true;
            }
            else
            {
                Logger.Log("ResourceInfo.Write() : Failed to write serialized packet data");
                return false;
            }
        }
    }
}
