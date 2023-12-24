namespace CATS
{
    public class Simplex
    {
        public uint frequency;
        public Modulation modulationType;
        public byte power;
        public byte[] encoded = new byte[1] { 0 };
        public Simplex(uint _frequency, Modulation _modulationType, float _power, bool powerKnown = true)
        {
            frequency = _frequency;
            modulationType = _modulationType;
            if(powerKnown)
            {
                power = (byte)(_power * 4);
            }
            else
            {
                power = 255;
            }
        }
        
        public Simplex(uint _frequency, Modulation _modulationType, bool powerKnown = false)
        {
            frequency = _frequency;
            modulationType = _modulationType;
            if(!powerKnown)
            {
                power = 255;
            }
        }
        
        public Simplex()
        {

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
            encoded[0] = 7;
            encoded[1] = 6;
            Array.Copy(BitConverter.GetBytes(frequency), 0, encoded, 2, 4);
            encoded[6] = (byte)modulationType;
            encoded[7] = power;
            return encoded;
        }

        public Simplex Decode(byte[] data)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, 2, temp, 0, 4);
            frequency = BitConverter.ToUInt32(temp);
            modulationType = (Modulation)data[6];
            power = data[7];
            return this;
        }
    }
}