namespace CATS
{
    public class Timestamp
    {
        public ulong _time;
        public byte[] _timeBytes;
        public Timestamp(ulong unixTime)
        {
            if(unixTime > 1099511627775)
            {
                return;
            }

            _time = unixTime;
            _timeBytes = BitConverter.GetBytes(unixTime);
        }

        public byte[] Encode()
        {
            byte[] encoded = new byte[7];
            return encoded;
        }
    }
}