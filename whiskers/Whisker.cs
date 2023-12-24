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
            int maxEnum = Enum.GetValues(typeof(WhiskerType)).Cast<int>().Max(); // get max int of enum
            for(int i = 0; i < packet.Length; i++)
            {
                if(packet[i] <= maxEnum)
                {
                    WhiskerType type = (WhiskerType)packet[i];
                    int length = packet[i] + 2;
                    yield return new Whisker(type, packet[i..(i + length)]);
                    i += length;
                }
            }
        }
    }
}