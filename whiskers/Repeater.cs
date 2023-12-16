using System.Net;

namespace CATS
{
    public class Repeater
    {
        public uint uplinkFreq;
        public uint downlinkFreq;
        public Modulation modulationType;
        public byte power;
        public short latitude;
        public short longitude;
        public string name;
        public byte[] toneBytes = new byte[4];
        public byte[] encoded = new byte[1];
        public Repeater(uint _uplink, uint _downlink, Modulation _modulationType, byte power, double latitude, double longitude, string _name)
        {
            name = _name;
        }

        public Repeater()
        {
            name = "";
        }

        public void SetTones(ToneInfo t)
        {
            uint bits = t.downlinkCode | (Convert.ToUInt32(t.downlinkDCS) << 11);
            bits |= (t.uplinkCode << 12) | (Convert.ToUInt32(t.uplinkDCS) << 23);
            toneBytes = BitConverter.GetBytes(bits);
        }

        public ToneInfo GetTones()
        {
            uint bits = BitConverter.ToUInt32(toneBytes);

            ToneInfo t = new ToneInfo();
            // bit magic
            t.uplinkDCS = (bits >> 23) != 0;
            t.downlinkDCS = (bits & 2048) != 0;
            t.uplinkCode = (bits >> 12) & 2047;
            t.downlinkCode = bits & 1023;
            return t;
        }

        public byte[] Encode()
        {
            encoded[0] = 8;
            encoded[1] = (byte)(name.Length + 17);
            Array.Copy(BitConverter.GetBytes(uplinkFreq), 0, encoded, 2, 4);
            Array.Copy(BitConverter.GetBytes(downlinkFreq), 0, encoded, 6, 4);
            encoded[10] = (byte)modulationType;
            Array.Copy(toneBytes, 0, encoded, 11, 3);
            encoded[14] = power;
            Array.Copy(BitConverter.GetBytes(latitude), 0, encoded, 15, 2);
            Array.Copy(BitConverter.GetBytes(longitude), 0, encoded, 17, 2);
            Array.Copy(System.Text.Encoding.UTF8.GetBytes(name), 0, encoded, 19, name.Length);
            return encoded;
        }

        public Repeater Decode(byte[] data)
        {
            return this;
        }
    }
}