namespace CATS
{
    public class Repeater
    {
        public byte[] encoded = new byte[1] { 0 };
        public Repeater()
        {

        }

        public byte[] Encode()
        {
            return encoded;
        }

        public Repeater Decode(byte[] data)
        {
            return this;
        }
    }
}