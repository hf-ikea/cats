namespace CATS
{
    public class Packet
    {
        public List<byte> bytes = new List<byte>(8191);

        public Packet()
        {

        }
        // after crc
        public byte[] SemiEncode()
        {
            ushort crc = CRC.GetChecksum(bytes);
            byte[] encoded = new byte[bytes.Count + 2];

            return encoded;
        }

        // encodeds packet without header
        public byte[] FullEncode()
        {
            byte[] data = this.SemiEncode();
            data = Whitener.Whiten(data);
            // LDPC !!!

            return Interleaver.Interleave(data);
        }

        public void SemiDecode(byte[] data)
        {
            ushort expectedCRC = CRC.GetChecksum(data[..^3]);
            ushort recievedCRC = BitConverter.ToUInt16(data.AsSpan()[^2..]);
            if(expectedCRC != recievedCRC) throw new Exception("CRC does not match!");
        }

        // decodes entire packet without header
        public void FullyDecode(byte[] data)
        {
            data = Interleaver.Deinterleave(data);
            // LDPC DECODE
            data = Whitener.Whiten(data);
        }
    }
}