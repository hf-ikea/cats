namespace CATS
{
    public class Timestamp
    {
        public ulong _time;
        public byte[] _timeBytes = new byte[5];
        public Timestamp(ulong unixTime)
        {
            _time = unixTime;
            if(unixTime > 1099511627775)
            {
                return;
            }
            Array.Copy(BitConverter.GetBytes(_time), _timeBytes, 5);
        }

        public Timestamp()
        {
            _time = 0;
        }

        public byte[] Encode()
        {
            byte[] encoded = new byte[7];
            encoded[0] = 1;
            encoded[1] = 5;
            Array.Copy(_timeBytes, 0, encoded, 2, 5);
            return encoded;
        }

        public Timestamp Decode(byte[] data)
        {
            _timeBytes = new byte[8];
            Array.Copy(data, 2, _timeBytes, 0, 5);
            _time = BitConverter.ToUInt64(_timeBytes);
            return this;
        }
    }
}