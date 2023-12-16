namespace CATS
{
    public class Repeater
    {
        public uint uplinkFreq;
        public uint downlinkFreq;
        public Modulation modulationType;
        public byte power;
        public double latitude;
        private short latitudeEncoded;
        public double longitude;
        private short longitudeEncoded;
        public string name;
        public byte[] toneBytes = new byte[3];
        public byte[] encoded = new byte[1];
        public Repeater(uint _uplink, uint _downlink, Modulation _modulationType, float _power, double _latitude, double _longitude, string _name, bool powerKnown = true)
        {
            uplinkFreq = _uplink;
            downlinkFreq = _downlink;
            modulationType = _modulationType;
            if(powerKnown)
            {
                power = (byte)(_power * 4);
            }
            else
            {
                power = 255;
            }
            latitudeEncoded = (short)(23860929.4222 * Math.Clamp(_latitude, -90, 90));
            latitude = latitudeEncoded;
            longitudeEncoded = (short)(11930464.7111 * Math.Clamp(_longitude, -180, 180));
            longitude = longitudeEncoded;
            name = _name;
        }

        public Repeater(uint _uplink, uint _downlink, Modulation _modulationType, double _latitude, double _longitude, string _name, bool powerKnown = false)
        {
            uplinkFreq = _uplink;
            downlinkFreq = _downlink;
            modulationType = _modulationType;
            if(!powerKnown)
            {
                power = 255;
            }
            latitudeEncoded = (short)(23860929.4222 * Math.Clamp(_latitude, -90, 90));
            latitude = latitudeEncoded;
            longitudeEncoded = (short)(11930464.7111 * Math.Clamp(_longitude, -180, 180));
            longitude = longitudeEncoded;
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
            Array.Copy(BitConverter.GetBytes(bits), 0, toneBytes, 0, 3);
        }

        public ToneInfo GetTones()
        {
            byte[] temp = new byte[4];
            Array.Copy(toneBytes, 0, temp, 0, 3);
            uint bits = BitConverter.ToUInt32(temp);
            ToneInfo t = new()
            {
                // bit magic
                uplinkDCS = (bits >> 23) != 0,
                downlinkDCS = (bits & 2048) != 0,
                uplinkCode = (bits >> 12) & 2047,
                downlinkCode = bits & 1023
            };

            // Console.WriteLine(t.uplinkDCS + " " + t.downlinkDCS + " " + t.uplinkCode + " " + t.downlinkCode);
            return t;
        }

        public float GetPower()
        {
            if(power == 255)
            {
                return 0;
            }
            return power / 4;
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
            Array.Copy(BitConverter.GetBytes(latitudeEncoded), 0, encoded, 15, 2);
            Array.Copy(BitConverter.GetBytes(longitudeEncoded), 0, encoded, 17, 2);
            Array.Copy(System.Text.Encoding.UTF8.GetBytes(name), 0, encoded, 19, name.Length);
            return encoded;
        }

        public Repeater Decode(byte[] data)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, 2, temp, 0, 4);
            uplinkFreq = BitConverter.ToUInt32(temp);
            Array.Copy(data, 6, temp, 0, 4);
            downlinkFreq = BitConverter.ToUInt32(temp);
            modulationType = (Modulation)data[10];
            Array.Copy(data, 11, toneBytes, 0, 3);
            power = data[14];

            Array.Copy(data, 15, temp, 0, 2);
            latitudeEncoded = BitConverter.ToInt16(temp);
            Array.Copy(data, 17, temp, 0, 2);
            longitudeEncoded = BitConverter.ToInt16(temp);

            temp = new byte[data[1] - 17];
            Array.Copy(data, 19, temp, 0, data[1] - 17);
            name = System.Text.Encoding.UTF8.GetString(temp);

            latitude = latitudeEncoded / 23860929.4222;
            longitude = longitudeEncoded / 11930464.7111;
            return this;
        }
    }
}