using Shared.Network;
using Shared.Packets;
using System;

namespace Shared.Contents
{
    public struct Item : ISerializableData
    {
        public ResourceType resourceType;
        public ushort id;
        public ushort quantity;

        public void Read(in ArraySegment<byte> segment, ref int count)
        {
            int c = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);


            resourceType = (ResourceType)BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);

            //ushort id
            id = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);

            //ushort quantity
            quantity = BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);
        }

        public bool Write(ArraySegment<byte> segment, ref int count)
        {
            int c = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;


            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)resourceType);
            c += sizeof(ushort);

            //ushort id
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), id);
            c += sizeof(ushort);

            //ushort quantity
            success &= BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), quantity);
            c += sizeof(ushort);


            ArraySegment<byte> result = SendBufferHelper.Close(c);

            if (success)
            {
                return true;
            }
            else
            {
                Logger.Log("Item.Write() : Failed to write serialized packet data");
                return false;
            }
        }
    }
}