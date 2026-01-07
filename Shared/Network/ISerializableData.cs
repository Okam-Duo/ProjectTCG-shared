namespace Shared.Network
{
    public interface ISerializableData
    {
        public void Read(ReadOnlySpan<byte> segment, ref int count);
        public bool Write(Span<byte> segment, ref int count);
    }
}
