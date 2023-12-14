namespace CATS
{
    public class Identification
    {
        public string call;
        private byte[] callBytes;
        public byte ssid;
        public ushort icon;
        private byte[] iconBytes;
        public byte[] encoded = new byte[1] { 0 };
        public Identification(string _call, byte _ssid, ushort _icon)
        {
            call = _call;
            callBytes = System.Text.Encoding.UTF8.GetBytes(call);
            ssid = _ssid;
            icon = _icon;
            iconBytes = BitConverter.GetBytes(icon);
        }

        public Identification()
        {
            call = "";
            callBytes = new byte[252];
            ssid = 0;
            icon = 0;
            iconBytes = new byte[2];

        }

        public byte[] Encode()
        {
            int callLength = callBytes.Length;
            encoded = new byte[5 + callLength];
            encoded[0] = 0;
            encoded[1] = (byte)(encoded.Length - 2);
            encoded[2] = iconBytes[0];
            encoded[3] = iconBytes[1];
            Array.Copy(callBytes, 0, encoded, 4, callLength);
            encoded[4 + callLength] = ssid;
            return encoded;
        }

        public Identification Decode(byte[] data)
        {
            for(int i = 0; i < data.Length - 5; i++)
            {
                callBytes[i] = data[i + 4];
            }
            call = System.Text.Encoding.UTF8.GetString(callBytes);
            ssid = data[data.Length - 1];
            iconBytes[0] = data[2];
            iconBytes[1] = data[3];
            icon = BitConverter.ToUInt16(iconBytes);
            return this;
        }
    }
}