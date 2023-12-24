namespace CATS
{
    public class Packet
    {
        public List<byte> bytes = new(8191);
        public bool hasIdentification = false;
        public bool hasTimestamp = false;
        public bool hasGPS = false;
        public bool hasRoute = false;

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

        public void SemiDecode(byte[] packet)
        {
            byte[] data = packet[..^3];
            ushort expectedCRC = CRC.GetChecksum(data);
            ushort recievedCRC = BitConverter.ToUInt16(packet.AsSpan()[^2..]);
            if(expectedCRC != recievedCRC) throw new Exception("CRC does not match!");

            if(data.Length > 8191) throw new Exception("Packet too long!");

            
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