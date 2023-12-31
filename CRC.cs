namespace CATS
{
    public class CRC
    {
        // 16 bit ibm sdlc crc
        // https://gist.github.com/bryc/79d1a62304773285317191f1ae5aa5b8
        // uses a reflected 0x1021 polynomial (0x8408)
        // inverted (xor with 0xFFFF)
        public static ushort GetChecksum(byte[] data)
        {
            uint t = 0;
            uint crc = 0xFFFF; // start state
            for(int i = 0; i < data.Length; i++)
            {
                crc &= 0xFFFF; // remove bits over 16
                t = crc ^ data[i];
                t = (t ^ (t << 4)) & 0xFF;
                crc = (crc >> 8) ^ (t << 8) ^ (t >> 4) ^ (t << 3);
            }
            return (ushort)~crc; // xor the output with 0xFFFF (invert)
        }

        public static ushort GetChecksum(List<byte> data)
        {
            uint t = 0;
            uint crc = 0xFFFF; // start state
            for(int i = 0; i < data.Count; i++)
            {
                crc &= 0xFFFF; // remove bits over 16
                t = crc ^ data[i];
                t = (t ^ (t << 4)) & 0xFF;
                crc = (crc >> 8) ^ (t << 8) ^ (t >> 4) ^ (t << 3);
            }
            return (ushort)~crc; // xor the output with 0xFFFF (invert)
        }
    }
}