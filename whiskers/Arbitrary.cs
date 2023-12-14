namespace CATS
{
    public class Arbitrary
    {
        public byte[] data;
        public byte[] encoded = new byte[1] { 0 };
        public Arbitrary(byte[] _data)
        {
            data = _data;
        }

        public Arbitrary()
        {
            data = new byte[1];
        }

        public byte[] Encode()
        {
            encoded = new byte[data.Length + 2];
            encoded[0] = 6;
            encoded[1] = (byte)data.Length;
            Array.Copy(data, 0, encoded, 2, data.Length);
            return encoded;
        }

        public Arbitrary Decode(byte[] _data)
        {
            data = _data[2..];
            return this;
        }
    }
}