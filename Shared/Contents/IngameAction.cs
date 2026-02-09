using Shared.Network;
using Shared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Shared.Contents
{
    //
    public struct IngameAction : ISerializableData
    {
        public IngameActionType actionType;
        public ISerializableData parameters;

        public IngameAction(IngameActionType actionType, ISerializableData parameters)
        {
            this.actionType = actionType;
            this.parameters = parameters;
        }

        public override string ToString()
        {
            XmlSerializer xml = new(typeof(IngameAction));
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


            //IngameActionType actionType
            actionType = (IngameActionType)BitConverter.ToUInt16(s.Slice(c, segment.Count - c));
            c += sizeof(ushort);

            //struct parameters
            //parameters.Read(segment, ref c);
#warning TODO : 타입에 따라 parameters를 다른 방식으로 읽기
            switch (actionType)
            {
                default:
                    Logger.Log($"{nameof(IngameAction)}.Read : 할당되지 않은 IngameActionType을 수신했습니다.");
                    break;
            }
        }

        public bool Write(ArraySegment<byte> segment, ref int c)
        {
            ushort len = 0;
            Span<byte> s = new Span<byte>(segment.Array, segment.Offset + c, segment.Count);
            bool success = true;

            //전체 사이즈 적을 공간 비워놓기
            c += sizeof(ushort);


            //IngameActionType actionType
            BitConverter.TryWriteBytes(s.Slice(c, s.Length - c), (ushort)actionType);
            c += sizeof(ushort);

            parameters.Write(new ArraySegment<byte>(segment.Array, segment.Offset + c, segment.Count - c), ref c);



            //전체 크기 삽입
            success &= BitConverter.TryWriteBytes(s, (ushort)c);

            if (success)
            {
                return true;
            }
            else
            {
                Logger.Log("IngameAction.Write() : Failed to write serialized data");
                return false;
            }
        }
    }
}
