namespace CATS
{
    public class Whitener
    {
        readonly byte[] white = new byte[16] {0xE9, 0xCF, 0x67, 0x20, 0x19, 0x1A, 0x07, 0xDC, 0xC0, 0x72, 0x79, 0x97, 0x51, 0xF7, 0xDD, 0x93};

        public byte[] Whiten(byte[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                data[i] ^= white[i % 15];
            }
            return data;
        }
    }
}
