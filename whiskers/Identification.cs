namespace CATS
{
    public class Identification
    {
        public string call;
        public byte ssid;
        public ushort icon;
        private byte[] iconBytes;
        public byte[] encoded = new byte[1] { 0 };
        public Identification(string _call, byte _ssid, ushort _icon)
        {
            call = _call;
            ssid = _ssid;
            icon = _icon;
            iconBytes = BitConverter.GetBytes(icon);
        }

        public Identification()
        {
            call = "";
            ssid = 0;
            icon = 0;
            iconBytes = new byte[2];

        }

        public byte[] Encode()
        {
            encoded = new byte[call.Length + 5];
            encoded[0] = 0;
            encoded[1] = (byte)(call.Length + 3);
            encoded[2] = iconBytes[0];
            encoded[3] = iconBytes[1];
            Array.Copy(System.Text.Encoding.UTF8.GetBytes(call), 0, encoded, 4, call.Length);
            encoded[4 + call.Length] = ssid;
            return encoded;
        }

        public Identification Decode(byte[] data)
        {
            byte[] callBytes = new byte[data.Length - 5];
            Array.Copy(data, 4, callBytes, 0, data.Length - 5);
            call = System.Text.Encoding.UTF8.GetString(callBytes);
            ssid = data[^1];
            iconBytes[0] = data[2];
            iconBytes[1] = data[3];
            icon = BitConverter.ToUInt16(iconBytes);
            return this;
        }

        public static implicit operator Identification?(Type? v)
        {
            throw new NotImplementedException();
        }
    }
}
