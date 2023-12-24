namespace CATS
{
    public class Packet
    {
        public List<byte> byteList = new(8189); // NOT encoded (no CRC, whiten, ldpc, interleave, header) this is just the bytes from the whiskers concat together LENGTH: 2 less than 8189 to accomodate the CRC check
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
            ushort crc = CRC.GetChecksum(byteList);
            byte[] encoded = new byte[byteList.Count + 2];
            if(encoded.Length > 8191) throw new Exception("Packet is too long!");
            Array.Copy(BitConverter.GetBytes(crc), 0, encoded, byteList.Count, 2);
            
            return encoded;
        }

        // encodeds packet without header
        public byte[] FullEncode()
        {
            byte[] packet = SemiEncode();
            packet = Whitener.Whiten(packet);
            packet = LDPC.Encode(packet);

            return Interleaver.Interleave(packet);
        }

        public List<Whisker> SemiDecode(byte[] packet)
        {
            byte[] data = packet[..^3];
            ushort expectedCRC = CRC.GetChecksum(data);
            ushort recievedCRC = BitConverter.ToUInt16(packet.AsSpan()[^2..]);
            if(expectedCRC != recievedCRC) throw new Exception("CRC does not match!");

            List<Whisker> whiskers = new();
            foreach(Whisker w in Whisker.GetWhiskers(data))
            {
                switch(w.type)
                {
                    case WhiskerType.Identification:
                        if(hasIdentification) throw new Exception("Duplicate identification whiskers!");
                        whiskers.Add(w);
                        break;
                    case WhiskerType.Timestamp:
                        if(hasTimestamp) throw new Exception("Duplicate timestamp whiskers!");
                        whiskers.Add(w);
                        break;
                    case WhiskerType.GPS:
                        if(hasGPS) throw new Exception("Duplicate GPS whiskers!");
                        whiskers.Add(w);
                        break;
                    case WhiskerType.Route:
                        if(hasRoute) throw new Exception("Duplicate route whiskers!");
                        whiskers.Add(w);
                        break;
                    default:
                        whiskers.Add(w);
                        break;
                }
            }

            return whiskers;
        }

        // decodes entire packet without header
        public List<Whisker> FullDecode(byte[] data)
        {
            data = Interleaver.Deinterleave(data);
            data = LDPC.Decode(data);
            data = Whitener.Whiten(data);
            return SemiDecode(data);
        }
    }
}