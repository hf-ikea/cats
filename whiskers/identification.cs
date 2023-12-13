using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace CATS
{
    public class Identification
    {
        public string _call;
        private byte[] _callBytes;
        public byte _ssid;
        public ushort _icon;
        private byte[] _iconBytes;
        public Identification(string call, byte ssid, ushort icon)
        {
            _call = call;
            _callBytes = System.Text.Encoding.UTF8.GetBytes(call);
            _ssid = ssid;
            _icon = icon;
            _iconBytes = BitConverter.GetBytes(_icon);
        }

        public Identification()
        {
            _call = "";
            _callBytes = new byte[252];
            _ssid = 0;
            _icon = 0;
            _iconBytes = new byte[2];

        }

        public byte[] Encode()
        {
            byte[] encoded = new byte[5 + _callBytes.Length];
            encoded[0] = 0;
            encoded[1] = (byte)encoded.Length;
            encoded[2] = _iconBytes[0];
            encoded[3] = _iconBytes[1];
            for(int i = 0; i < _callBytes.Length; i++)
            {
                encoded[i + 4] = _callBytes[i];
            }
            encoded[4 + _callBytes.Length] = _ssid;
            return encoded;
        }

        public Identification Decode(byte[] data)
        {
            for(int i = 0; i < data.Length - 5; i++)
            {
                _callBytes[i] = data[i + 4];
            }
            _call = System.Text.Encoding.UTF8.GetString(_callBytes);
            _ssid = data[data.Length - 1];
            _iconBytes[0] = data[2];
            _iconBytes[1] = data[3];
            _icon = BitConverter.ToUInt16(_iconBytes);
            return this;
        }
    }
}