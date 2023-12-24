namespace CATS
{
    public class Packet
    {
        // after crc
        public static byte[] SemiEncode(List<Whisker> whiskerList)
        {
            List<byte> bytes = new(8189);
            foreach(Whisker w in whiskerList)
            {
                bytes.Add((byte)w.type);
                bytes.AddRange(w.data);
            }

            if(bytes.Count + 2 > 8191) throw new Exception("Packet is too long!");

            byte[] encoded = new byte[bytes.Count + 2];
            Array.Copy(BitConverter.GetBytes(CRC.GetChecksum(bytes)), 0, encoded, bytes.Count, 2);
            
            return encoded;
        }

        // encodes packet without header
        public static byte[] FullEncode(List<Whisker> whiskerList)
        {
            byte[] packet = SemiEncode(whiskerList);
            packet = Whitener.Whiten(packet);
            packet = LDPC.Encode(packet);

            return Interleaver.Interleave(packet);
        }

        public static List<Whisker> SemiDecode(byte[] packet)
        {
            byte[] data = packet[..^3];
            ushort expectedCRC = CRC.GetChecksum(data);
            ushort recievedCRC = BitConverter.ToUInt16(packet.AsSpan()[^2..]);
            if(expectedCRC != recievedCRC) throw new Exception("CRC does not match!");

            bool hasIdentification = false;
            bool hasTimestamp = false;
            bool hasGPS = false;
            bool hasRoute = false;

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
        public static List<Whisker> FullDecode(byte[] data)
        {
            data = Interleaver.Deinterleave(data);
            data = LDPC.Decode(data);
            data = Whitener.Whiten(data);
            return SemiDecode(data);
        }

        // split a comment into several whiskers if it is too long
        public static List<Whisker> SplitComment(string comment)
        {
            List<Whisker> list = new();
            for(int i = 0; i < comment.Length; i += 255)
            {
                list.Add(new Whisker(WhiskerType.Comment, new Comment(comment[i..(i + 255)]).Encode()));
            }

            return list;
        }
    }
}