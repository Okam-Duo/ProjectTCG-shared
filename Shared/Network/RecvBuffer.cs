using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Network
{
    public class RecvBuffer
    {
        private ArraySegment<byte> _buffer;
        private int _readPosition;
        private int _writePosition;

        public RecvBuffer(int bufferSize)
        {
            _buffer = new ArraySegment<byte>(new byte[bufferSize], 0, bufferSize);
        }

        public int DataSize { get { return _writePosition - _readPosition; } }
        public int FreeSize { get { return _buffer.Count - _writePosition; } }

        public ArraySegment<byte> ReadSegment
        {
            get
            {
                return new ArraySegment<byte>(_buffer.Array, _buffer.Offset, DataSize);
            }
        }

        public ArraySegment<byte> WriteSegment
        {
            get
            {
                return new ArraySegment<byte>(_buffer.Array, _buffer.Offset + _writePosition, FreeSize);
            }
        }

        public void Clean()
        {
            if (DataSize == 0)
            {
                //남은 데이터가 없으면 데이터를 복사하지 않고 커서 위치만 초기화
                _readPosition = 0;
                _writePosition = 0;
            }
            else
            {
                Array.Copy(_buffer.Array, _buffer.Offset + _writePosition, _buffer.Array, _buffer.Offset, DataSize);
                _readPosition = 0;
                _writePosition = DataSize;
            }
        }

        public bool OnRead(int numOfBytes)
        {
            if (numOfBytes > DataSize)
            {
                return false;
            }

            _readPosition += numOfBytes;
            return true;
        }

        public bool OnWrite(int numOfBytes)
        {
            if (numOfBytes > FreeSize)
            {
                return false;
            }

            _writePosition += numOfBytes;
            return true;
        }
    }
}