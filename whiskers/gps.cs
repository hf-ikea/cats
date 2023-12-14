namespace CATS
{
    public class GPS
    {
        private int latitudeEncoded;
        public double latitude;
        private int longitudeEncoded;
        public double longitude;
        public Half altitude;
        public byte locationError;
        private byte headingEncoded;
        public double heading;
        public Half speed;
        public byte[] encoded = new byte[1] { 0 };
        public GPS(double _latitude, double _longitude, double _altitude, byte _locationError, double _heading, float _speed)
        {
            latitudeEncoded = (int)(23860929.4222 * Math.Clamp(_latitude, -90, 90));
            latitude = _latitude;
            longitudeEncoded = (int)(11930464.7111 * Math.Clamp(_longitude, -180, 180));
            longitude = _longitude;
            altitude = (Half)_altitude;
            locationError = _locationError;
            headingEncoded = (byte)(Math.Clamp(_heading, 0, 360) * (128 / 180.0));
            heading = _heading;
            speed = (Half)_speed;
        }
        
        public GPS()
        {

        }

        public byte[] Encode()
        {
            encoded = new byte[16];
            encoded[0] = 2;
            encoded[1] = 14;
            Array.Copy(BitConverter.GetBytes(latitudeEncoded), 0, encoded, 2, 4);
            Array.Copy(BitConverter.GetBytes(longitudeEncoded), 0, encoded, 6, 4);
            Array.Copy(BitConverter.GetBytes(altitude), 0, encoded, 10, 2);
            encoded[12] = locationError;
            encoded[13] = headingEncoded;
            Array.Copy(BitConverter.GetBytes(speed), 0, encoded, 14, 2);
            return encoded;
        }

        public GPS Decode(byte[] data)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, 2, temp, 0, 4);
            latitudeEncoded = BitConverter.ToInt32(temp);
            Array.Copy(data, 6, temp, 0, 4);
            longitudeEncoded = BitConverter.ToInt32(temp);
            Array.Copy(data, 10, temp, 0, 2);
            altitude = BitConverter.ToHalf(temp);
            locationError = data[12];
            headingEncoded = data[13];
            Array.Copy(data, 14, temp, 0, 2);
            speed = BitConverter.ToHalf(temp);

            latitude = latitudeEncoded / 23860929.4222;
            longitude = longitudeEncoded / 11930464.7111;
            heading = (headingEncoded / 128.0) * 180;
            return this;
        }
    }
}