using System.Text;

namespace CATS
{
    public class Destination
    {
        public string call;
        public byte ssid;
        public bool isAck;
        public byte ackID;
        public byte[] encoded = new byte[1] { 0 };
        public Destination(string _call, int _ssid)
        {
            call = _call;
            ssid = (byte)_ssid;
            isAck = false;
        }
        public Destination(string _call, int _ssid, bool _isAck, byte _ackID)
        {
            call = _call;
            ssid = (byte)_ssid;
            if(_ackID > 127 || (_isAck && _ackID == 0)) return;
            isAck = _isAck;
            ackID = _ackID;
        }

        public Destination()
        {
            call = "";
        }

        public byte[] Encode()
        {
            encoded = new byte[call.Length + 4];
            encoded[0] = 5;
            encoded[1] = (byte)(call.Length + 2);
            if(isAck)
            {
                encoded[2] = (byte)(128 | ackID);
            } else
            {
                encoded[2] = ackID;
            }
            Array.Copy(System.Text.Encoding.UTF8.GetBytes(call), 0, encoded, 3, call.Length);
            encoded[^1] = ssid;
            return encoded;
        }

        public Destination Decode(byte[] data)
        {
            isAck = data[2] > 127;
            ackID = (byte)(data[2] & 127);
            byte[] callBytes = new byte[data[2] - 2];
            Array.Copy(data, 3, callBytes, 0, data[1] - 2);
            call = System.Text.Encoding.UTF8.GetString(callBytes);
            ssid = data[^1];
            return this;
        }
    }
}