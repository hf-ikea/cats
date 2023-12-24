namespace CATS
{
    public enum WhiskerType
    {
        Identification,
        Timestamp,
        GPS,
        Comment,
        Route,
        Destination,
        Arbitrary,
        Simplex,
        Repeater,
        NodeInfo,
    }

    public class Whisker
    {
        public WhiskerType type;
        public byte[] data;

        public Whisker(WhiskerType _type, byte[] _data)
        {
            type = _type;
            data = _data;
        }

        public static IEnumerable<Whisker> GetWhiskers(byte[] packet)
        {
            for(int i = 0; i < packet.Length; i++)
            {
                switch(packet[i])
                {
                    case 1:
                        byte length = packet[++i];
                        
                        break;
                }
            }
        }
    }
}