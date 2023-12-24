namespace CATS
{
    public class Whitener
    {
        public static byte[] Whiten(byte[] data)
        {
            ushort start_state = 59855;
            ushort state = start_state;

            for(int i = 0; i < data.Length; i++)
            {
                byte output = 0;
                for(int j = 8; j--> 0;)
                {
                    output |= (byte)((state & 1) << j);
                    state = AdvanceLFSR(state);
                }
                data[i] ^= output;
            }
            
            return data;
        }

        public static ushort AdvanceLFSR(ushort state)
        {
            ushort lsb = (ushort)(state & 1);
            state >>= 1;
            if(lsb > 0) { state ^= 0xB400; }
            return state;
        }
    }
}
