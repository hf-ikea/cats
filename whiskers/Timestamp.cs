namespace CATS
{
    public class Timestamp
    {
        public ulong time;
        public byte[] timeBytes = new byte[5];
        public byte[] encoded = new byte[1] { 0 };
        public Timestamp(ulong _unixTime)
        {
            time = _unixTime;
            if(_unixTime > 1099511627775)
            {
                return;
            }
            Array.Copy(BitConverter.GetBytes(time), timeBytes, 5);
        }

        public Timestamp()
        {
            time = 0;
        }

        public byte[] Encode()
        {
            encoded = new byte[7];
            encoded[0] = 1;
            encoded[1] = 5;
            Array.Copy(timeBytes, 0, encoded, 2, 5);
            return encoded;
        }

        public Timestamp Decode(byte[] data)
        {
            timeBytes = new byte[8];
            Array.Copy(data, 2, timeBytes, 0, 5);
            time = BitConverter.ToUInt64(timeBytes);
            return this;
        }
    }
}
